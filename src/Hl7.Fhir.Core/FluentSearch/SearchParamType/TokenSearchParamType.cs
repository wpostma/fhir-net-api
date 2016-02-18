namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class TokenSearchParamType : BaseSearchParamType
    {
        public ISearchParamType Text(string value)
        {
            AddToParameters(value, FluentQueryConstants.SEARCH_TEXT);
            return this;
        }

        public ISearchParamType Matches(string value)
        {
            AddToParameters(value);
            return this;
        }

        public ISearchParamType Not(object value)
        {
            AddToParameters(value.ToString(), FluentQueryConstants.SEARCH_NOT);
            return this;
        }

        public ISearchParamType Above(string code, string system)
        {
            var parameter = GetSystemAndCode(system, code);
            AddToParameters(parameter, FluentQueryConstants.SEARCH_ABOVE);
            return this;
        }

        public ISearchParamType Below(string code, string system)
        {
            var parameter = GetSystemAndCode(system, code);
            AddToParameters(parameter, FluentQueryConstants.SEARCH_BELOW);
            return this;
        }

        public ISearchParamType In(object uri)
        {
            AddToParameters(uri.ToString(), FluentQueryConstants.SEARCH_IN);
            return this;
        }

        public ISearchParamType NotIn(object uri)
        {
            AddToParameters(uri.ToString(), FluentQueryConstants.SEARCH_NOT_IN);
            return this;
        }

        private static string GetSystemAndCode(string system, string code)
        {
            return string.Format("{0}|{1}", system, code);
        }
    }
}
