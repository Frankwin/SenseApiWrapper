using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SenseApi;

namespace SenseApiTests
{
    public class TestBase
    {
        protected SenseApiWrapper SenseApi;
        
        public static IConfigurationRoot Config { get; private set; }
        public int MonitorId { get; set; }

        [TestInitialize]
        public async Task TestClassSetup()
        {
            SenseApi = new SenseApiWrapper();

            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (Config["email"] == "" || Config["password"] == "")
            {
                throw new KeyNotFoundException("The email and/or password are not configured in the appsettings.json file.");
            }

            if (Config["accesstoken"] == "" || Config["monitor-ids"] == "")
            {
                var result = await SenseApi.Authenticate(Config["email"], Config["password"]);
                if (result.AccessToken != null)
                {
                    Config["accesstoken"] = result.AccessToken;
                    Config["monitor-ids"] = string.Join(",", result.Monitors.Select(x => x.Id));
                    Config["user-id"] = result.UserId.ToString();
                }
            }

            // Just use the first monitor found in all tests.
            MonitorId = int.Parse(Config["monitor-ids"].Split(",")[0]);
        }
    }
}
