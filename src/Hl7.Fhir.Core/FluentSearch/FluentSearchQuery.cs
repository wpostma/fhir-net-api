using System;
using System.Linq;
using System.Linq.Expressions;
using Hl7.Fhir.FluentSearch.ResourcesSearchParams;
using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.FluentSearch
{
    public class FluentSearchQuery<T> : IFluentSearchQuery<T> where T : ISearchParam, new()
    {
        public FluentSearchQuery()
        {
            Filters = new SearchParams();
        }
 
        public SearchParams Filters { get; set; }

        public string ChainedUri { get; set; }

        public IFluentSearchQuery<T> Where(Expression<Func<T, ISearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("whereFilter");

            var parameterType = new T();
            var compiledFunc = filter.Compile();
            var functionResult = compiledFunc.Invoke(parameterType);
            var values = functionResult.GetResult();
            var property = string.Empty;
            if (filter.Body is MethodCallExpression)
            {
                var bodyExpression = filter.Body as MethodCallExpression;
                property = GetMemberExpressionProperty(bodyExpression);
            }

            foreach (var value in values)
            {
                if (!string.IsNullOrEmpty(value.Item2))
                    property += value.Item2;
                Filters.Add(property, value.Item1);
            }
            return this;
        }
     

        public IFluentSearchQuery<T> Include(Expression<Func<T, ReferenceSearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("includeFilter");

            if (!(filter.Body is MemberExpression)) return this;

            var bodyExpression = filter.Body as MemberExpression;
            var property = GetMemberExpressionProperty(bodyExpression);
            var type = GetBaseClassGenericType();

            Filters.Include.Add(ComposeIncludeUri(type, property));

            return this;
        }


        public IFluentSearchQuery<T> SummaryOnly(bool summaryOnly = true)
        {
            Filters.Summary = summaryOnly ? SummaryType.True : SummaryType.False;
            return this;
        }

        public IFluentSearchQuery<T> LimitTo(int count)
        {
            Filters.Count = count;
            return this;
        }

        public IFluentSearchQuery<T> Custom(string customQuery)
        {
            if (customQuery == null) throw Error.ArgumentNull("customQuery");
            Filters.Query = customQuery;
            return this;
        }


        public IFluentSearchQuery<T> OrderBy(Expression<Func<T, ISearchParamType>> filter, SortOrder order = SortOrder.Ascending)
        {
            if (filter == null)
                throw Error.ArgumentNull("orderbyFilter");

            if (!(filter.Body is MemberExpression)) return this;

            var bodyExpression = filter.Body as MemberExpression;
            var property = GetMemberExpressionProperty(bodyExpression);

            Filters.Sort.Add(Tuple.Create(property, order));

            return this;
        }

        public IFluentSearchQuery<T> Contained(ContainedSearch value = ContainedSearch.False, ContainedResult type = ContainedResult.Container)
        {
            Filters.Add(FluentQueryConstants.SEARCH_CONTAINED, value.ToString().ToLowerInvariant());
            Filters.Add(FluentQueryConstants.SEARCH_CONTAINED_TYPE, type.ToString().ToLowerInvariant());
            return this;
        }

        public ChainedContext<T, TChainedResource> ChainedProperty<TChainedResource>(Expression<Func<T, ReferenceSearchParamType>> filter, TChainedResource chainedPropertyResource) where TChainedResource : ISearchParam, new()
        {
            if (filter == null || chainedPropertyResource == null)
                throw Error.ArgumentNull("chainedProperty");

            if (filter.Body is MemberExpression)
            {
                var bodyExpression = filter.Body as MemberExpression;
                var property = GetMemberExpressionProperty(bodyExpression);
                var name = GetBaseClassGenericType();
                var chainedPropertyName = GetBaseClassGenericTypeOfChainedProperty<TChainedResource>();

                ChainedUri += ComposeChainedPropertyUrl(name, property, chainedPropertyName);

                return new ChainedContext<T, TChainedResource>(this);
            }

            return null;
        }


        public SearchParams ToQuery()
        {
            return Filters;
        }

        private string GetBaseClassGenericTypeOfChainedProperty<TChainedResource>() where TChainedResource : new()
        {
            var chainedParameterType = new TChainedResource();
            var chainedBaseClass = chainedParameterType.GetType().BaseType;

            if (chainedBaseClass == null) return string.Empty;

            var argument = chainedBaseClass.GetGenericArguments().FirstOrDefault();
            return argument != null ? argument.Name : string.Empty;
        }

        public IFluentSearchQuery<T> RevIncludePatient(Expression<Func<PatientSearchParam, ReferenceSearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("revIncludePatient");

            if (!(filter.Body is MemberExpression)) return this;

            var bodyExpression = filter.Body as MemberExpression;
            var property = GetMemberExpressionProperty(bodyExpression);
            Filters.RevInclude.Add(ComposeRevIncludeUrl(FluentQueryConstants.Patient, property));

            return this;
        }

        public IFluentSearchQuery<T> RevIncludeOrganization(Expression<Func<OrganizationSearchParam, ReferenceSearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("revIncludeOrganization");

            if (!(filter.Body is MemberExpression)) return this;

            var bodyExpression = filter.Body as MemberExpression;
            var property = GetMemberExpressionProperty(bodyExpression);
            Filters.RevInclude.Add(ComposeRevIncludeUrl(FluentQueryConstants.Organization, property));

            return this;
        }

        public IFluentSearchQuery<T> RevIncludeMedication(Expression<Func<MedicationSearchParam, ReferenceSearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("revIncludeMedication");

            if (!(filter.Body is MemberExpression)) return this;

            var bodyExpression = filter.Body as MemberExpression;
            var property = GetMemberExpressionProperty(bodyExpression);
            Filters.RevInclude.Add(ComposeRevIncludeUrl(FluentQueryConstants.Medication, property));

            return this;
        }

        private string GetMemberExpressionProperty(MemberExpression expression)
        {
            if (expression == null) return string.Empty;

            var memberExpression = expression.Member;
            var property = memberExpression.Name.ToLowerInvariant();
            return property;
        }

        private string GetMemberExpressionProperty(MethodCallExpression expression)
        {
            var memberExpression = expression.Object as MemberExpression;
            return memberExpression != null ? memberExpression.Member.Name.ToLowerInvariant() : string.Empty;
        }

        private static string ComposeRevIncludeUrl(string resource, string property)
        {
            return string.Format("{0}{1}{2}", resource, FluentQueryConstants.SEARCH_MODIFIERSEPARATOR, property);
        }

        private static string ComposeChainedPropertyUrl(string type, string property, string resource)
        {
            return string.Format("{0}{1}{2}{3}{4}", type, FluentQueryConstants.SEARCH_REFERENCESEPARATOR, property, FluentQueryConstants.SEARCH_MODIFIERSEPARATOR, resource);
        }

        private static string ComposeIncludeUri(string type, string property)
        {
            return string.Format("{0}{1}{2}", type, FluentQueryConstants.SEARCH_REFERENCESEPARATOR, property);
        }

        private static string GetBaseClassGenericType()
        {
            var parameterType = new T();
            var baseClass = parameterType.GetType().BaseType;

            if (baseClass == null) return string.Empty;

            var argument = baseClass.GetGenericArguments().FirstOrDefault();

            return argument != null ? argument.Name : string.Empty;
        }

    }

}
