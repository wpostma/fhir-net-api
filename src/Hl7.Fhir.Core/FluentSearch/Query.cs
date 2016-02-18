using Hl7.Fhir.FluentSearch.ResourcesSearchParams;

namespace Hl7.Fhir.FluentSearch
{
    public  class Query
    {
        public Query()
        {
            Patient = new FluentSearchQuery<PatientSearchParam>();
            Medication = new FluentSearchQuery<MedicationSearchParam>();
            Organization = new FluentSearchQuery<OrganizationSearchParam>();
        }

        public FluentSearchQuery<PatientSearchParam> Patient { get; set; }
        public FluentSearchQuery<MedicationSearchParam> Medication { get; set; }
        public FluentSearchQuery<OrganizationSearchParam> Organization { get; set; }
    }
}
