using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.FluentSearch.ResourcesSearchParams
{
    public class BaseSearchParam : ISearchParam
    {
        protected BaseSearchParam()
        {
            Id = new TokenSearchParamType();
            LastUpdated = new DateTimeSearchParamType();
        }
        public TokenSearchParamType Id { get; set; }
        public DateTimeSearchParamType LastUpdated { get; set; }
    }
}
