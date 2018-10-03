using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class TimelineDataTests : TestBase
    {
        [TestMethod]
        public async Task GetTimelineData()
        {
            var result = await SenseApi.GetTimelineData(int.Parse(Config["user-id"]));

            Assert.IsTrue(result.UserId == int.Parse(Config["user-id"]));
            Assert.IsTrue(result.Items.Count > 0);
        }
    }
}