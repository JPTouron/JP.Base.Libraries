using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD.Base.Errors.Common;


namespace TD.Base.Errors.Common.Tests
{
	[TestClass]
	public class ErrorLocationTests 
	{

		[TestMethod]
		public void ErrorLocation_Create_ShouldCreateAValidErrorLocationObject()
		{
			var line = 3;
			var method = "thefaillngMethod()";
			var fileName = "theFilename";

			var errLocation = new ErrorLocation(line, method, fileName);

			errLocation.FileName.Should().Be(fileName, null, null);
			errLocation.Method.Should().Be(method, null, null);
			errLocation.Line.Should().Be(line, null, null);


		}

	}
}
