using System;
using System.Collections.Generic;

namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public interface ISearchParamType
    {
        ISearchParamType IsMissing(bool isMissing);
        IList<Tuple<string,string>> GetResult();
    }
}
