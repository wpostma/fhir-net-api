using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.FluentSearch.ResourcesSearchParams
{
    public class OrganizationSearchParam : BaseSearchParam
    {
        public OrganizationSearchParam()
        {
            Identifier = new TokenSearchParamType();
            Active = new TokenSearchParamType();
            Type = new TokenSearchParamType();
            Phonetic = new StringSearchParamType();
            Name = new StringSearchParamType();
            PartOf = new ReferenceSearchParamType();
        }
        public TokenSearchParamType Identifier { get; set; }
        public TokenSearchParamType Active { get; set; }
        public TokenSearchParamType Type { get; set; }

        public StringSearchParamType Phonetic { get; set; }
        public StringSearchParamType Name { get; set; }

        public ReferenceSearchParamType PartOf { get; set; }
    }
}
