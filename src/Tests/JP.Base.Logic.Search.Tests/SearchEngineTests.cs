using JP.Base.Logic.Search.Tests.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JP.Base.Logic.Search.Tests
{
    [TestClass]
    public class SearchEngineTests
    {
        private SearchEngineImpl engine;

        [TestMethod]
        public void TestMethod1()
        {
            engine = new SearchEngineImpl(new BaseSearchParams
            {
                DefaultSortingField = "Cost",
                SortAndFilter =
                new SortAndFilterData { }
            });

            var query = engine.GetSearchQuery();

            Assert.IsNotNull(query);
        }
    }
}