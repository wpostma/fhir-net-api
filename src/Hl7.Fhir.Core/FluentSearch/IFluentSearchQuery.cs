using System;
using System.Linq.Expressions;
using Hl7.Fhir.FluentSearch.ResourcesSearchParams;
using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.FluentSearch
{
    public interface IFluentSearchQuery<T> where T: ISearchParam, new()
    {
        IFluentSearchQuery<T> Where(Expression<Func<T, ISearchParamType>> filter);
        IFluentSearchQuery<T> Include(Expression<Func<T, ReferenceSearchParamType>> filter);
        IFluentSearchQuery<T> SummaryOnly(bool summaryOnly = true);
        IFluentSearchQuery<T> LimitTo(int count);
        IFluentSearchQuery<T> Custom(string query);
        IFluentSearchQuery<T> OrderBy(Expression<Func<T, ISearchParamType>> filter, SortOrder sortOrder);
        IFluentSearchQuery<T> Contained(ContainedSearch value = ContainedSearch.False, ContainedResult type = ContainedResult.Container);
        IFluentSearchQuery<T> RevIncludePatient(Expression<Func<PatientSearchParam, ReferenceSearchParamType>> filter);
        IFluentSearchQuery<T> RevIncludeOrganization(Expression<Func<OrganizationSearchParam, ReferenceSearchParamType>> filter);
        IFluentSearchQuery<T> RevIncludeMedication(Expression<Func<MedicationSearchParam, ReferenceSearchParamType>> filter);
        ChainedContext<T, TChainedResource> ChainedProperty<TChainedResource>(Expression<Func<T, ReferenceSearchParamType>> filter,
            TChainedResource chainedPropertyResource) where TChainedResource : ISearchParam, new();
        SearchParams ToQuery();
    }
}
