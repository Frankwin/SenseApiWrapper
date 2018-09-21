using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class DeviceTests : TestBase
    {
        [TestMethod]
        public async Task GetDeviceListTest()
        {
            var result = await SenseApi.GetDeviceList(int.Parse(Config["monitor-id"]));

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetDeviceDetails()
        {
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(int.Parse(Config["monitor-id"]));
            }

            var rnd = new Random();
            var r = rnd.Next(SenseApi.DeviceList.Count);

            var result = await SenseApi.GetDeviceDetails(int.Parse(Config["monitor-id"]), SenseApi.DeviceList[r].Id);

            Assert.IsTrue(result.Device.Id == SenseApi.DeviceList[r].Id);
        }

        [TestMethod]
        public async Task GetAlwaysOnDetails()
        {
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(int.Parse(Config["monitor-id"]));
            }

            var result = await SenseApi.GetAlwaysOnDetails(int.Parse(Config["monitor-id"]));

            Assert.IsTrue(result.Device.Id.Contains("always_on", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public async Task GetOtherDetails()
        {
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(int.Parse(Config["monitor-id"]));
            }

            var result = await SenseApi.GetOtherDetails(int.Parse(Config["monitor-id"]));

            Assert.IsTrue(result.Device.Id.Contains("unknown", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public async Task MakeSureDeviceListGetsUpdatedWithDeviceDetailsTest()
        {
                await SenseApi.GetDeviceList(int.Parse(Config["monitor-id"]));
                var deviceDetails = await SenseApi.GetDeviceDetails(int.Parse(Config["monitor-id"]), SenseApi.DeviceList.First().Id);

                Assert.IsTrue(SenseApi.DeviceList.Find(x => x.Id == deviceDetails.Device.Id).LastState != null);
        }
    }
}
