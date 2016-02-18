using System;

namespace Hl7.Fhir.FluentSearch.SearchParamType
{
    public class DateTimeSearchParamType: BaseSearchParamType
    {
        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        public ISearchParamType After(DateTime value)
        {
            var finalValue = GetFormatDateTime(value);
            AddToParameters(finalValue, FluentQueryConstants.SEARCH_GREATER_THAN);
            return this;
        }

        public ISearchParamType AfterOrEquals(DateTime value)
        {
            var finalValue = GetFormatDateTime(value);
            AddToParameters(finalValue, FluentQueryConstants.SEARCH_GREATER_THAN_OR_EQUAL);
            return this;
        }

        public ISearchParamType Before(DateTime value)
        {
            var finalValue = GetFormatDateTime(value);
            AddToParameters(finalValue, FluentQueryConstants.SEARCH_LESS_THAN);
            return this;
        }

        public ISearchParamType BeforeOrEquals(DateTime value)
        {
            var finalValue = GetFormatDateTime(value);
            AddToParameters(finalValue, FluentQueryConstants.SEARCH_LESS_THAN_OR_EQUAL);
            return this;
        }

        public ISearchParamType Matches(DateTime value)
        {
            var finalValue = GetFormatDateTime(value);
            AddToParameters(finalValue);
            return this;
        }

        private static string GetFormatDateTime(DateTime value)
        {
            return value.ToString(value.TimeOfDay == TimeSpan.Zero ? DateFormat : DateTimeFormat);
        }
    }
}
