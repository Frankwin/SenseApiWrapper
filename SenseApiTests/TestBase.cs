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

        [TestInitialize]
        public void TestClassSetup()
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

            if (Config["accesstoken"] == "")
            {
                Task.Run(async () =>
                {
                    var result = await SenseApi.Authenticate(Config["email"], Config["password"]);
                    if (result.AccessToken != null)
                    {
                        Config["accesstoken"] = result.AccessToken;
                        Config.Providers.First().Set("accesstoken", result.AccessToken);
                    }
                }).GetAwaiter().GetResult();
            }
        }
    }
}
