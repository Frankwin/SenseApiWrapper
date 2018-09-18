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
                if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
                {
                    await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
                }

                var rnd = new Random();
                var r = rnd.Next(SenseApi.DeviceList.Count);

                var result = await SenseApi.GetDeviceDetails(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList[r].Id);

                Assert.IsTrue(result.Device.Id == SenseApi.DeviceList[r].Id);

            }).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void MakeSureDeviceListGetsUpdatedWithDeviceDetailsTest()
        {
            Task.Run(async () =>
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
                var deviceDetails = await SenseApi.GetDeviceDetails(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList.First().Id);

                Assert.IsTrue(SenseApi.DeviceList.Find(x => x.Id == deviceDetails.Device.Id).LastState != null);

            }).GetAwaiter().GetResult();
        }
    }
}
