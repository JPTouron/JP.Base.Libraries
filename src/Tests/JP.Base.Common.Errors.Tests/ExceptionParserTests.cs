using FluentAssertions;
using JP.Base.Common.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TD.Base.Errors.Common.Tests
{
    [TestClass]
    public class ExceptionDataTests
    {
        [TestMethod]
        public void ExceptionData_CreateExceptionData_ShouldCreateAValidExceptionData()
        {
            var obj = default(ExceptionData);
            var obj2 = default(ExceptionData);
            var obj3 = default(ExceptionData);

            try
            {
                throw new Exception("This is a Test");
            }
            catch (Exception ex)
            {
                obj = new ExceptionData(ex);
            }

            try
            {
                throw new OutOfMemoryException("This is 2 a Test");
            }
            catch (Exception ex)
            {
                obj2 = new ExceptionData(ex, true);
            }

            try
            {
                throw new IndexOutOfRangeException("This is a Test #3");
            }
            catch (Exception ex)
            {
                obj3 = new ExceptionData(ex);
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