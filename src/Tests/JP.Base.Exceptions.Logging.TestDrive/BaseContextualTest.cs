using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TD.Base.Logging.ErrorLogging.Tests
{
    public class BaseContextualTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}