namespace Hl7.Fhir.FluentSearch
{
    public static class FluentQueryConstants
    {
        public const string SEARCH_MISSING_TRUE = "true";
        public const string SEARCH_MISSING_FALSE = "false";
        public const string SEARCH_EXACT = ":exact";
        public const string SEARCH_MISSING = ":missing";
        public const string SEARCH_CONTAINED_BOTH = "both";

        public const string SEARCH_CONTAINED_TYPE = "_containedType";
        public const string SEARCH_CONTAINED = "_contained";
        public const string SEARCH_ASCENDING = "asc";
        public const string SEARCH_DESCENDING = "desc";
        public const char SEARCH_MODIFIERSEPARATOR = ':';
        public const char SEARCH_REFERENCESEPARATOR = '.';
        public const string SEARCH_BELOW = ":below";
        public const string SEARCH_ABOVE = ":above";
        public const string SEARCH_TEXT = ":text";
        public const string SEARCH_APPROXIMATELY = "~";
        public const string SEARCH_GREATER_THAN_OR_EQUAL = ">=";
        public const string SEARCH_LESS_THAN = "<";
        public const string SEARCH_LESS_THAN_OR_EQUAL = "<=";
        public const string SEARCH_GREATER_THAN = ">";
        public const string SEARCH_NOT_EQUAL = "!=";
        public const string SEARCH_NOT = ":not";
        public const string SEARCH_NOT_IN = ":not-in"; 
        public const string SEARCH_IN = ":in";

       public const string Medication = "Medication";
       public const string Patient = "Patient";
       public const string Organization = "Organization";
    }
}
