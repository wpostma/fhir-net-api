using Hl7.Fhir.FluentSearch.SearchParamType;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.FluentSearch.ResourcesSearchParams
{
    public class PatientSearchParam : BaseSearchParam
    {
        public PatientSearchParam()
        {
            BirthDate = new DateTimeSearchParamType();
            Link = new ReferenceSearchParamType();
            Provider = new ReferenceSearchParamType();
            Address = new StringSearchParamType();
            Family = new StringSearchParamType();
            Given = new StringSearchParamType();
            Name = new StringSearchParamType();
            Phonetic = new StringSearchParamType();
            Telecom = new StringSearchParamType();
            Active = new TokenSearchParamType();
            AnimalBreed = new TokenSearchParamType();
            AnimalSpecies = new TokenSearchParamType();
            Gender = new TokenSearchParamType();
            Identifier = new TokenSearchParamType();
            Language = new TokenSearchParamType();
            Organization = new ReferenceSearchParamType();
        }
        public DateTimeSearchParamType BirthDate { get; set; }

        public ReferenceSearchParamType Link { get; set; }
        public ReferenceSearchParamType Provider { get; set; }
        public ReferenceSearchParamType Organization { get; set; }

        public StringSearchParamType Address { get; set; }
        public StringSearchParamType Family { get; set; }
        public StringSearchParamType Given { get; set; }
        public StringSearchParamType Name { get; set; }
        public StringSearchParamType Phonetic { get; set; }
        public StringSearchParamType Telecom { get; set; }

        public TokenSearchParamType Active { get; set; }
        public TokenSearchParamType AnimalBreed { get; set; }
        public TokenSearchParamType AnimalSpecies { get; set; }   
        public TokenSearchParamType Gender { get; set; }
        public TokenSearchParamType Identifier { get; set; }
        public TokenSearchParamType Language { get; set; }
    }
}
