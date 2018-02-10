using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD.Base.Logging.ErrorLogging.Tests;
using FluentAssertions;
using TD.Base.Errors.Common;

namespace TD.Base.Logging.ErrorLogging.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class ErrorObjectTests : BaseContextualTest
	{

		[TestMethod]
		public void ErrorObject_CreateErrorObject_ShouldCreateAValidErrorObject()
		{
			var obj = default(ErrorObject);
			var obj2 = default(ErrorObject);
			var obj3 = default(ErrorObject);

			try
			{
				throw new Exception("This is a Test");
			}
			catch (Exception ex)
			{
				obj = new ErrorObject(ex);
			}

			try
			{
				throw new OutOfMemoryException("This is 2 a Test");
			}
			catch (Exception ex)
			{
				obj2 = new ErrorObject(ex, true);
			}

			try
			{
				throw new IndexOutOfRangeException("This is a Test #3");
			}
			catch (Exception ex)
			{
				obj3 = new ErrorObject(ex);
				obj3.IsUnhandledException = true;
			}

			obj.Should().NotBeNull(null, null);
			obj.IsUnhandledException.Should().BeFalse(null, null);
			obj.AssemblyInfo.Should().NotBeNullOrEmpty(null, null);
			obj.CurrenException.Should().BeOfType<Exception>(null, null);
			obj.CurrenException.Message.Should().Be("This is a Test", null, null);
			obj.EnvironmentInfo.Should().NotBeNullOrEmpty(null, null);
			obj.ErrorLocations.Count.Should().BeGreaterThan(0, null, null);
			obj.ExceptionText.Should().NotBeNullOrEmpty(null, null);
			obj.ExceptionType.Should().Be("Exception Type:           " + obj.CurrenException.GetType().FullName, null, null);
			obj.StackTrace.Should().NotBeNullOrEmpty(null, null);


			obj2.Should().NotBeNull(null, null);
			obj2.IsUnhandledException.Should().BeTrue(null, null);
			obj2.AssemblyInfo.Should().NotBeNullOrEmpty(null, null);
			obj2.CurrenException.Should().BeOfType<OutOfMemoryException>(null, null);
			obj2.CurrenException.Message.Should().Be("This is 2 a Test", null, null);
			obj2.EnvironmentInfo.Should().NotBeNullOrEmpty(null, null);
			obj2.ErrorLocations.Count.Should().BeGreaterThan(0, null, null);
			obj2.ExceptionText.Should().NotBeNullOrEmpty(null, null);
			obj2.ExceptionType.Should().Be("Exception Type:           " + obj2.CurrenException.GetType().FullName, null, null);
			obj.StackTrace.Should().NotBeNullOrEmpty(null, null);

			obj3.IsUnhandledException.Should().BeTrue(null, null);
		}

	}
}
