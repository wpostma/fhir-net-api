using System;
using System.Collections.Generic;

namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class BaseSearchParamType : ISearchParamType
    {
        private IList<Tuple<string,string>> Parameters { get; set; } 

        public BaseSearchParamType()
        {
            Parameters = new List<Tuple<string, string>>();
        }

        public ISearchParamType IsMissing(bool isMissing)
        {
            AddToParameters(isMissing.ToString(), FluentQueryConstants.SEARCH_MISSING);
            return this;
        }

        protected void AddToParameters(string value, string modifier = null)
        {
            Parameters.Add(Tuple.Create(value, modifier));
        }

        public IList<Tuple<string, string>> GetResult()
        {
            return Parameters;
        }
    }
}
