namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class NumberSearchParamType : BaseSearchParamType
    {
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

        public ISearchParamType EqualsWith(float value)
        {
            AddToParameters(value.ToString());
            return this;
        }
    }
}
