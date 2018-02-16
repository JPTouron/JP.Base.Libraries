using FluentAssertions;
using JP.Base.Common.Exceptions.Parsing;
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
                obj3 = new ExceptionData(ex,true );
            }

            obj.Should().NotBeNull(null, null);
            obj.IsUnhandledException.Should().BeFalse(null, null);
            obj.GetAssemblyInfo().Should().NotBeNullOrEmpty(null, null);
            obj.CurrentException.Should().BeOfType<Exception>(null, null);
            obj.CurrentException.Message.Should().Be("This is a Test", null, null);
            obj.SysInfoToString().Should().NotBeNullOrEmpty(null, null);
            obj.GetErrorLocations().Count.Should().BeGreaterThan(0, null, null);
            obj.ExceptionToString().Should().NotBeNullOrEmpty(null, null);
            obj.GetExceptionType().Should().Be("Exception Type:           " + obj.CurrentException.GetType().FullName, null, null);
            obj.GetEnhancedStackTrace().Should().NotBeNullOrEmpty(null, null);

            obj2.Should().NotBeNull(null, null);
            obj2.IsUnhandledException.Should().BeTrue(null, null);
            obj2.GetAssemblyInfo().Should().NotBeNullOrEmpty(null, null);
            obj2.CurrentException.Should().BeOfType<OutOfMemoryException>(null, null);
            obj2.CurrentException.Message.Should().Be("This is 2 a Test", null, null);
            obj2.SysInfoToString().Should().NotBeNullOrEmpty(null, null);
            obj2.GetErrorLocations().Count.Should().BeGreaterThan(0, null, null);
            obj2.ExceptionToString().Should().NotBeNullOrEmpty(null, null);
            obj2.GetExceptionType().Should().Be("Exception Type:           " + obj2.CurrentException.GetType().FullName, null, null);
            obj.GetEnhancedStackTrace().Should().NotBeNullOrEmpty(null, null);

            obj3.IsUnhandledException.Should().BeTrue(null, null);
        }
    }
}