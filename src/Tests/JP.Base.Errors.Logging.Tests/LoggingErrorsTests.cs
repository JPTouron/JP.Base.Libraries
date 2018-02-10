using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD.Base.Logging.ErrorLogging.Tests;
using FluentAssertions;
using System.IO;
using TD.Base.Logging.ErrorLogging.Tests.Support;
using TD.Base.Errors.Logging;

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
			logErrors.EventLogLoggingErrors= "random2";
			logErrors.FileLoggingErrors = "random3";

			logErrors.Should().NotBeNull(null, null);
			logErrors.DataBaseLoggingErrors.Should().Be("random", null, null);
			logErrors.EventLogLoggingErrors.Should().Be("random2", null, null);
			logErrors.FileLoggingErrors.Should().Be("random3", null, null);

		}

	}
}
