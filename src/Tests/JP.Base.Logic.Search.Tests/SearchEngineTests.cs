using JP.Base.Logic.Search.Tests.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JP.Base.Logic.Search.Tests
{
    [TestClass]
    public class SearchEngineTests
    {
        private SearchEngineImpl engine;

        [TestMethod]
        public void Verify_Initializatoin_Of_Sortandfilterdata()
        {
            var saf = new SortAndFilterData { };

            engine = new SearchEngineImpl(new BaseSearchParams
            {
                DefaultSortingField = "Cost",
                SortAndFilter = saf

            });

            var query = engine.GetSearchQuery();

            Assert.IsNotNull(query);
            Assert.IsTrue(saf.Page == 1);
            Assert.IsTrue(saf.PageSize == 10);
            Assert.IsTrue(saf.SearchString == null);
            Assert.IsTrue(saf.SortOrder == "asc");
            Assert.IsTrue(saf.SortField == "Cost");


        }



        [TestMethod]
        public void trysortorderswitch()
        {
            var saf = new SortAndFilterData { SortOrder = "" };

            engine = new SearchEngineImpl(new BaseSearchParams
            {
                SortAndFilter = saf

            });

            var query = engine.GetSearchQuery();

            Assert.IsNotNull(query);
            Assert.IsTrue(saf.SortOrder == "asc");

            saf.SortOrder = "desc";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "desc");
            
            saf.SortOrder = "asc";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "asc");
            
            saf.SortOrder = "";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "asc");


            saf.SortOrder = "asc";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "asc");

            saf.SortOrder = "desc";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "desc");

            saf.SortOrder = "";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "asc");

            saf.SortOrder = "asc";
            query = engine.GetSearchQuery();

            Assert.IsTrue(saf.SortOrder == "asc");


        }

    }
}