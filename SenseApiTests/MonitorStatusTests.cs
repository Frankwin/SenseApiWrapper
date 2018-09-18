using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class MonitorStatusTests : TestBase
    {
        [TestMethod]
        public void CheckMonitorStatus()
        {
            Task.Run(async () =>
            {
                var result = await SenseApi.GetMonitorStatus(SenseApi.AuthorizationResponse.Monitors.First().Id);

                Assert.IsTrue(result.MonitorInfo.Serial != null);

            }).GetAwaiter().GetResult();
        }
    }
}
