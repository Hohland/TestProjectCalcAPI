using Calc.Web.Services;
using System;
using Xunit;

namespace Calc.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void AddValidationTest()
        {
            var service = new CalcService(new LoggerMock());
            Assert.Throws<ApplicationException>(() =>
            {
                service.Add(new[] { 1, float.PositiveInfinity });
            });
        }
    }
}
