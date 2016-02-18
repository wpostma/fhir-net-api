namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class QuantitySearchParamType : BaseSearchParamType
    {
        public ISearchParamType Approximately(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_APPROXIMATELY);
            return this;
        }

        public ISearchParamType Exactly(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_EXACT);
            return this;
        }

        public ISearchParamType GreaterThan(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_GREATER_THAN);
            return this;
        }

        public ISearchParamType GreaterThanOrEqual(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_GREATER_THAN_OR_EQUAL);
            return this;
        }

        public ISearchParamType LessThan(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_LESS_THAN);
            return this;
        }

        public ISearchParamType LessThanOrEqual(float value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_LESS_THAN_OR_EQUAL);
            return this;
        }

    }
}
