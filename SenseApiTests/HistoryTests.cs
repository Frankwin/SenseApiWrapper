using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseApi.Enums;

namespace SenseApiTests
{
    [TestClass]
    public class HistoryTests : TestBase
    {
        [TestMethod]
        public async Task GetMonitorHistoryInSeconds()
        {
            const int sampleCount = 86400;
            var result = await SenseApi.GetMonitorHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, Granularity.Second, DateTime.Now.AddDays(-1), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetMonitorHistoryInMinutes()
        {
            const int sampleCount = 1440;
            var result = await SenseApi.GetMonitorHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, Granularity.Minute, DateTime.Now.AddDays(-1), sampleCount );

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetMonitorHistoryInHours()
        {
            const int sampleCount = 720;
            var result = await SenseApi.GetMonitorHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, Granularity.Hour, DateTime.Now.AddDays(-30), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetMonitorHistoryInDays()
        {
            const int sampleCount = 365;
            var result = await SenseApi.GetMonitorHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, Granularity.Day, DateTime.Now.AddDays(-365), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetDeviceHistoryInSeconds()
        {
            const int sampleCount = 86400;
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
            }

            var rnd = new Random();
            var r = rnd.Next(SenseApi.DeviceList.Count);

            var result = await SenseApi.GetDeviceHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList[r].Id, Granularity.Second, DateTime.Now.AddDays(-1), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetDeviceHistoryInMinutes()
        {
            const int sampleCount = 1440;
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
            }

            var rnd = new Random();
            var r = rnd.Next(SenseApi.DeviceList.Count);

            var result = await SenseApi.GetDeviceHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList[r].Id, Granularity.Minute, DateTime.Now.AddDays(-1), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetDeviceHistoryInHours()
        {
            const int sampleCount = 720;
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
            }

            var rnd = new Random();
            var r = rnd.Next(SenseApi.DeviceList.Count);

            var result = await SenseApi.GetDeviceHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList[r].Id, Granularity.Hour, DateTime.Now.AddDays(-30), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }

        [TestMethod]
        public async Task GetDeviceHistoryInDays()
        {
            const int sampleCount = 365;
            if (SenseApi.DeviceList == null || SenseApi.DeviceList.Count == 0)
            {
                await SenseApi.GetDeviceList(SenseApi.AuthorizationResponse.Monitors.First().Id);
            }

            var rnd = new Random();
            var r = rnd.Next(SenseApi.DeviceList.Count);

            var result = await SenseApi.GetDeviceHistory(SenseApi.AuthorizationResponse.Monitors.First().Id, SenseApi.DeviceList[r].Id, Granularity.Day, DateTime.Now.AddDays(-365), sampleCount);

            Assert.IsTrue(result.Totals.Count == sampleCount);
        }
    }
}
