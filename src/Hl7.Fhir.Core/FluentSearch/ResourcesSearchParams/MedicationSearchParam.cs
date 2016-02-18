using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.FluentSearch.ResourcesSearchParams
{
    public class MedicationSearchParam : BaseSearchParam
    {
        public MedicationSearchParam()
        {
            Code = new TokenSearchParamType();
            Container = new TokenSearchParamType();
            Content = new ReferenceSearchParamType();
            Form = new TokenSearchParamType();
            Ingredient = new ReferenceSearchParamType();
            Manufacturer = new ReferenceSearchParamType();
            Name = new StringSearchParamType();
        }
        public TokenSearchParamType Code { get; private set; }
        public TokenSearchParamType Container { get; private set; }
        public ReferenceSearchParamType Content { get; private set; }
        public TokenSearchParamType Form { get; private set; }
        public ReferenceSearchParamType Ingredient { get; private set; }
        public ReferenceSearchParamType Manufacturer { get; private set; }
        public StringSearchParamType Name { get; private set; }
    }
}
