using FluentAssertions;
using JP.Base.Common.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TD.Base.Logging.ErrorLogging.Tests
{
    [TestClass]
    public class ExceptionLocationDataTests
    {
        [TestMethod]
        public void ExceptionLocationData_Create_ShouldCreateAValidExceptionLocationDataObject()
        {
            var line = 3;
            var method = "thefaillngMethod()";
            var fileName = "theFilename";

            var errLocation = new ExceptionLocationData(line, method, fileName);

            errLocation.FileName.Should().Be(fileName, null, null);
            errLocation.Method.Should().Be(method, null, null);
            errLocation.Line.Should().Be(line, null, null);
        }
    }
}