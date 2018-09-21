using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseApi.Enums;

namespace SenseApiTests
{
    [TestClass]
    public class TrendDataTests : TestBase
    {
        [TestMethod]
        public async Task GetUsageTrendDataForA1HourInterval()
        {
            var result = await SenseApi.GetUsageTrendData(int.Parse(Config["monitor-id"]), Granularity.Hour, DateTime.Now.AddDays(-1));

            Assert.IsTrue(result.Steps == 60);
            Assert.IsTrue(result.Scale == "hour");
        }
    }
}
