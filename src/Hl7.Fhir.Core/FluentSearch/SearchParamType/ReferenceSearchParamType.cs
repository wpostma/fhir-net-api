namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class ReferenceSearchParamType : BaseSearchParamType
    {
        public ISearchParamType HasId(object value)
        {
            AddToParameters(value.ToString());
            return this;
        }

        public ISearchParamType HasUrl(object value)
        {
            AddToParameters(value.ToString());
            return this;
        }
    }
}
