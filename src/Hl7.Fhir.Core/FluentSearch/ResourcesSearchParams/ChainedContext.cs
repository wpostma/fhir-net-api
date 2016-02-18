using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.FluentSearch.ResourcesSearchParams
{
    public class ChainedContext<TParentResource, TResource>
        where TParentResource : ISearchParam, new() 
        where TResource : new()
    {
        private FluentSearchQuery<TParentResource> ParentContext { get; set; }
        public ChainedContext(FluentSearchQuery<TParentResource> context)
        {
            ParentContext = context;
        }

        public ChainedContext<TParentResource, TChainedResource> ChainedProperty<TChainedResource>(Expression<Func<TResource, ReferenceSearchParamType>> filter, TChainedResource chainedPropertyResource) where TChainedResource : ISearchParam, new()
        {
            if (filter == null || chainedPropertyResource == null) 
                throw Error.ArgumentNull("chainedProperty");

            var property = GetProperty(filter);
            var chainedPropertyName = GetBaseClassGenericTypeOfChainedProperty<TChainedResource>();
            ParentContext.ChainedUri += ComposeChainedPropertyUri(property, chainedPropertyName);

            return new ChainedContext<TParentResource, TChainedResource>(ParentContext);
        }


        public FluentSearchQuery<TParentResource> WhereChained(Expression<Func<TResource, ISearchParamType>> filter)
        {
            if (filter == null)
                throw Error.ArgumentNull("whereChained");

            if (!(filter.Body is MethodCallExpression)) return ParentContext;

            var values = GetExpressionResult(filter);
            var property = GetProperty(filter.Body as MethodCallExpression);
            var result = ComposeWhereUrl(ParentContext.ChainedUri, property);
            ParentContext.ChainedUri = string.Empty;

            foreach (var value in values)
            {
                if (!string.IsNullOrEmpty(value.Item2))
                    result += value.Item2;
                ParentContext.Filters.Add(result, value.Item1);
            }
            return ParentContext;
        }

        private string GetBaseClassGenericTypeOfChainedProperty<TChainedResource>() where TChainedResource : new()
        {
            var chainedParameterType = new TChainedResource();
            var chainedBaseClass = chainedParameterType.GetType().BaseType;

            if (chainedBaseClass == null) return string.Empty;

            var argument = chainedBaseClass.GetGenericArguments().FirstOrDefault();
            return argument != null ? argument.Name : string.Empty;
        }

        private IEnumerable<Tuple<string, string>> GetExpressionResult(Expression<Func<TResource, ISearchParamType>> filter)
        {
            var parameterType = new TResource();
            var compiledFunc = filter.Compile();
            var functionResult = compiledFunc.Invoke(parameterType);
            return functionResult.GetResult();
        }

        private static string ComposeWhereUrl(string url, string property)
        {
            return string.Format("{0}{1}{2}", url, FluentQueryConstants.SEARCH_REFERENCESEPARATOR, property);
        }

        private static string ComposeChainedPropertyUri(string property, string resource)
        {
            return string.Format("{0}{1}{2}{3}", FluentQueryConstants.SEARCH_REFERENCESEPARATOR
                                               , property.ToLowerInvariant()
                                               , FluentQueryConstants.SEARCH_MODIFIERSEPARATOR
                                               , resource);
        }

        private static string GetProperty(MethodCallExpression expression)
        {
            var memberExpression = expression.Object as MemberExpression;
            return memberExpression != null ? memberExpression.Member.Name.ToLowerInvariant() : string.Empty;
        }

        private static string GetProperty(Expression<Func<TResource, ReferenceSearchParamType>> filter)
        {
            var bodyExpression = filter.Body as MemberExpression;
            if (bodyExpression == null) return string.Empty;
            var memberExpression = bodyExpression.Member;
            return memberExpression.Name;
        }
    }
}
