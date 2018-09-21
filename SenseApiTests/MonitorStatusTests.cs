using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class MonitorStatusTests : TestBase
    {
        [TestMethod]
        public async Task CheckMonitorStatus()
        {
            var result = await SenseApi.GetMonitorStatus(int.Parse(Config["monitor-ids"]));

            Assert.IsTrue(result.MonitorInfo.Serial != null);
        }
    }
}
