using System;
using System.Linq;
using Hl7.Fhir.FluentSearch;
using Hl7.Fhir.FluentSearch.ResourcesSearchParams;
using Hl7.Fhir.FluentSearch.SearchParamType;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resource = Hl7.Fhir.Model.Resource;

namespace Hl7.Fhir.Rest
{
    [TestClass]
    public class FluentSearchTests
    {
        [TestMethod]
        public void Where()
        {

            var queryMatches = new TestQuery().SomeResource.Where(x => x.String.Matches("Donald")).ToQuery();
            Assert.IsNotNull(queryMatches.Parameters.FirstOrDefault());
            Assert.AreEqual(queryMatches.Parameters.FirstOrDefault().Item1, "string");
            Assert.AreEqual(queryMatches.Parameters.FirstOrDefault().Item2, "Donald");


            var queryBefore = new TestQuery().SomeResource.Where(x => x.DateTime.Before(DateTime.Now)).ToQuery();
            Assert.IsNotNull(queryBefore.Parameters.FirstOrDefault());
            Assert.AreEqual(queryBefore.Parameters.FirstOrDefault().Item1, "datetime<");
            Assert.AreEqual(queryBefore.Parameters.FirstOrDefault().Item2, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFilter()
        {
            var query = new TestQuery().SomeResource.Where(null);
        }

        [TestMethod]
        public void Include()
        {
            var queryInclude = new TestQuery().SomeResource.Include(x => x.Reference).ToQuery();
            Assert.IsNotNull(queryInclude.Include.FirstOrDefault());
            Assert.AreEqual(queryInclude.Include.FirstOrDefault(), "Resource.reference");
        }

        [TestMethod]
        public void SummaryOnly()
        {
            var querySummary = new TestQuery().SomeResource.SummaryOnly(false).ToQuery();
            Assert.AreEqual(querySummary.Summary, SummaryType.False);
        }

        [TestMethod]
        public void LimitTo()
        {
            var queryCount = new TestQuery().SomeResource.LimitTo(4).ToQuery();
            Assert.AreEqual(queryCount.Count, 4);
        }

        [TestMethod]
        public void Custom()
        {
            var queryCustom = new TestQuery().SomeResource.Custom("myQuery").ToQuery();
            Assert.AreEqual(queryCustom.Query, "myQuery");
        }

        [TestMethod]
        public void OrderBy()
        {
            var queryOrderAsc = new TestQuery().SomeResource.OrderBy(x => x.String).ToQuery();
            Assert.IsNotNull(queryOrderAsc.Sort.FirstOrDefault());
            Assert.AreEqual(queryOrderAsc.Sort.FirstOrDefault().Item1, "string");
            Assert.AreEqual(queryOrderAsc.Sort.FirstOrDefault().Item2, SortOrder.Ascending);

            var queryOrderDesc = new TestQuery().SomeResource.OrderBy(x => x.String, SortOrder.Descending).ToQuery();
            Assert.IsNotNull(queryOrderDesc.Sort.FirstOrDefault());
            Assert.AreEqual(queryOrderDesc.Sort.FirstOrDefault().Item1, "string");
            Assert.AreEqual(queryOrderDesc.Sort.FirstOrDefault().Item2, SortOrder.Descending);
        }

        [TestMethod]
        public void Contained()
        {
            var queryNotContainedContainerType = new TestQuery().SomeResource.Contained().ToQuery();
            Assert.IsNotNull(queryNotContainedContainerType.Contained);
            Assert.AreEqual(queryNotContainedContainerType.Contained.Value, ContainedSearch.False);
            Assert.AreEqual(queryNotContainedContainerType.ContainedType.Value, ContainedResult.Container);

            var queryContainedContainedType = new TestQuery().SomeResource.Contained(ContainedSearch.True, ContainedResult.Contained).ToQuery();
            Assert.IsNotNull(queryContainedContainedType.Contained);
            Assert.AreEqual(queryContainedContainedType.Contained.Value, ContainedSearch.True);
            Assert.AreEqual(queryContainedContainedType.ContainedType.Value, ContainedResult.Contained);
        }

        [TestMethod]
        public void RevInclude()
        {
            var queryRevInclude = new Query().Patient.RevIncludePatient(x => x.Organization).ToQuery();
            Assert.IsNotNull(queryRevInclude.RevInclude.FirstOrDefault());
            Assert.AreEqual(queryRevInclude.RevInclude.FirstOrDefault(), "Patient:organization");
        }
        
        [TestMethod]
        public void ChainedProperty()
        {
            var query =
                new Query().Patient.ChainedProperty(x => x.Organization, Chained.Organization)
                    .WhereChained(x => x.Name.MatchesExactly("FHIR"))
                    .ToQuery();
            Assert.IsNotNull(query.Parameters.FirstOrDefault());
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item1, "Patient.organization:Organization.name:exact");
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item2, "FHIR"); 
        }

        [TestMethod]
        public void StringSearchParamTypeMethods()
        {
            var queryMatches = new TestQuery().SomeResource.Where(x => x.String.Matches("Donald")).ToQuery();
            Assert.IsNotNull(queryMatches.Parameters.FirstOrDefault());
            Assert.AreEqual(queryMatches.Parameters.FirstOrDefault().Item1, "string");
            Assert.AreEqual(queryMatches.Parameters.FirstOrDefault().Item2, "Donald");

            var queryMatchesExactly = new TestQuery().SomeResource.Where(x => x.String.MatchesExactly("Donald")).ToQuery();
            Assert.IsNotNull(queryMatchesExactly.Parameters.FirstOrDefault());
            Assert.AreEqual(queryMatchesExactly.Parameters.FirstOrDefault().Item1, "string:exact");
            Assert.AreEqual(queryMatchesExactly.Parameters.FirstOrDefault().Item2, "Donald");
        }

        [TestMethod]
        public void DateTimeSearchParamTypeMethods()
        {
            var queryAfter = new TestQuery().SomeResource.Where(x => x.DateTime.After(DateTime.Now)).ToQuery();
            Assert.IsNotNull(queryAfter.Parameters.FirstOrDefault());
            Assert.AreEqual(queryAfter.Parameters.FirstOrDefault().Item1, "datetime>");
            Assert.AreEqual(queryAfter.Parameters.FirstOrDefault().Item2, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

            var queryBeforeOrEquals = new TestQuery().SomeResource.Where(x => x.DateTime.BeforeOrEquals(DateTime.Now)).ToQuery();
            Assert.IsNotNull(queryBeforeOrEquals.Parameters.FirstOrDefault());
            Assert.AreEqual(queryBeforeOrEquals.Parameters.FirstOrDefault().Item1, "datetime<=");
            Assert.AreEqual(queryBeforeOrEquals.Parameters.FirstOrDefault().Item2, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        [TestMethod]
        public void TokenSearchParamTypeMethods()
        {
            var queryNot = new TestQuery().SomeResource.Where(x => x.Token.Not("F")).ToQuery();
            Assert.IsNotNull(queryNot.Parameters.FirstOrDefault());
            Assert.AreEqual(queryNot.Parameters.FirstOrDefault().Item1, "token:not");
            Assert.AreEqual(queryNot.Parameters.FirstOrDefault().Item2, "F");

            var queryAbove = new TestQuery().SomeResource.Where(x => x.Token.Above("F","http://something.com")).ToQuery();
            Assert.IsNotNull(queryAbove.Parameters.FirstOrDefault());
            Assert.AreEqual(queryAbove.Parameters.FirstOrDefault().Item1, "token:above");
            Assert.AreEqual(queryAbove.Parameters.FirstOrDefault().Item2, "http://something.com|F");
        }

        [TestMethod]
        public void UriSearchParamTypeMethods()
        {
            var queryAbove = new TestQuery().SomeResource.Where(x => x.Uri.Above("http://something.com")).ToQuery();
            Assert.IsNotNull(queryAbove.Parameters.FirstOrDefault());
            Assert.AreEqual(queryAbove.Parameters.FirstOrDefault().Item1, "uri:above");
            Assert.AreEqual(queryAbove.Parameters.FirstOrDefault().Item2, "http://something.com");
        }

        [TestMethod]
        public void ReferenceSearchParamTypeMethods()
        {
            var query = new TestQuery().SomeResource.Where(x => x.Reference.HasId(1)).ToQuery();
            Assert.IsNotNull(query.Parameters.FirstOrDefault());
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item1, "reference");
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item2, "1");
        }

        [TestMethod]
        public void QuantitySearchParamTypeMethods()
        {
            var query = new TestQuery().SomeResource.Where(x => x.Quantity.GreaterThanOrEqual(1)).ToQuery();
            Assert.IsNotNull(query.Parameters.FirstOrDefault());
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item1, "quantity>=");
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item2, "1");
        }


        [TestMethod]
        public void BaseSearchParamTypeMethods()
        {
            var query = new TestQuery().SomeResource.Where(x => x.DateTime.IsMissing(true)).ToQuery();
            Assert.IsNotNull(query.Parameters.FirstOrDefault());
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item1, "datetime:missing");
            Assert.AreEqual(query.Parameters.FirstOrDefault().Item2, "True");
        }

        [TestMethod]
        public void NumberSearchParamTypeMethods()
        {
            //>
            var queryGreaterThan = new TestQuery().SomeResource.Where(x => x.Number.GreaterThan(2)).ToQuery();
            Assert.IsNotNull(queryGreaterThan.Parameters.FirstOrDefault());
            Assert.AreEqual(queryGreaterThan.Parameters.FirstOrDefault().Item1, "number>");
            Assert.AreEqual(queryGreaterThan.Parameters.FirstOrDefault().Item2, "2");

            //<=
            var queryLessThanOrEqual = new TestQuery().SomeResource.Where(x => x.Number.LessThanOrEqual(6)).ToQuery();
            Assert.IsNotNull(queryLessThanOrEqual.Parameters.FirstOrDefault());
            Assert.AreEqual(queryLessThanOrEqual.Parameters.FirstOrDefault().Item1, "number<=");
            Assert.AreEqual(queryLessThanOrEqual.Parameters.FirstOrDefault().Item2, "6");

        }
    }


    public class SearchParam : BaseSearchParam, ISearchParam
    {
        public SearchParam()
        {
            Token = new TokenSearchParamType();
            Number = new NumberSearchParamType();
            String = new StringSearchParamType();
            Uri = new UriSearchParamType();
            Reference = new ReferenceSearchParamType();
            Quantity = new QuantitySearchParamType();
            DateTime = new DateTimeSearchParamType();
        }
        public TokenSearchParamType Token { get; private set; }
        public NumberSearchParamType Number { get; private set; }
        public StringSearchParamType String { get; private set; }
        public UriSearchParamType Uri { get; private set; }
        public ReferenceSearchParamType Reference { get; private set; }
        public QuantitySearchParamType Quantity { get; private set; }
        public DateTimeSearchParamType DateTime { get; private set; }
    }

    public class TestQuery
    {
        public TestQuery()
        {
            SomeResource = new FluentSearchQuery<SearchParam>();
        }

        public FluentSearchQuery<SearchParam> SomeResource { get; set; }
    }
}
