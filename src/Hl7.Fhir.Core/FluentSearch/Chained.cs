using Hl7.Fhir.FluentSearch.ResourcesSearchParams;

namespace Hl7.Fhir.FluentSearch
{
    public static class Chained
    {
        public static PatientSearchParam Patient
        {
            get { return new PatientSearchParam(); }
        }

        public static OrganizationSearchParam Organization
        {
            get { return new OrganizationSearchParam(); }
        }

        public static MedicationSearchParam Medication
        {
            get { return new MedicationSearchParam(); }
        }
    }
}
