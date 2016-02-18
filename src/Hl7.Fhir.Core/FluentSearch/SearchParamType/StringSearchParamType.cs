namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class StringSearchParamType : BaseSearchParamType
    {
        public ISearchParamType MatchesExactly(string value)
        {
            AddToParameters(value, FluentQueryConstants.SEARCH_EXACT);
            return this;
        }

        public ISearchParamType Matches(string value)
        {
            AddToParameters(value);
            return this;
        }
    }
}
