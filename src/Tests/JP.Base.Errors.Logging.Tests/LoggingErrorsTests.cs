using FluentAssertions;
using JP.Base.Errors.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TD.Base.Logging.ErrorLogging.Tests
{
    [TestClass]
    public class LoggingErrorsTests
    {
        [TestMethod]
        public void LoggingErrors_Create_ShouldCreateAValidLoggingErrorsObject()
        {
            var logErrors = new LoggingErrors();

            logErrors.DataBaseLoggingErrors = "random";
            logErrors.EventLogLoggingErrors = "random2";
            logErrors.FileLoggingErrors = "random3";

            logErrors.Should().NotBeNull(null, null);
            logErrors.DataBaseLoggingErrors.Should().Be("random", null, null);
            logErrors.EventLogLoggingErrors.Should().Be("random2", null, null);
            logErrors.FileLoggingErrors.Should().Be("random3", null, null);
        }
    }
}