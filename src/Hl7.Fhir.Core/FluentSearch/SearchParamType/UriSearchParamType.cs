namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class UriSearchParamType: BaseSearchParamType
    {
        public ISearchParamType Above(string value)
        {
            AddToParameters(value, FluentQueryConstants.SEARCH_ABOVE);
            return this;
        }

        public ISearchParamType Below(string value)
        {
            AddToParameters(value, FluentQueryConstants.SEARCH_BELOW);
            return this;
        }
    }
}
