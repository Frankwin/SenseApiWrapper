using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class DeviceTests : TestBase
    {
        [TestMethod]
        public void GetDeviceListTest()
        {
            Task.Run(async () =>
            {
                var result = await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);

                Assert.IsTrue(result.Count > 0);

            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetDeviceDetails()
        {
            Task.Run(async () =>
            {
                var result = await SenseApi.GetDeviceDetails(SenseApi.AuthorizationResponse.Monitors.First().Id, "e9c5fdbd");

                Assert.IsTrue(result.Device.Id == "e9c5fdbd");

            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void MakeSureDeviceListGetsUpdatedWithDeviceDetailsTest()
        {
            Task.Run(async () =>
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
                await SenseApi.GetDeviceDetails(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList.First().Id);

                Assert.IsTrue(SenseApi.DeviceList.First().LastStateTime != null);

            }).GetAwaiter().GetResult();
        }
    }
}
