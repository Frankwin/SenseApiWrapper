using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            if (Config["accesstoken"] == "" || SenseApi.AuthorizationResponse == null)
            {
                var result = await SenseApi.Authenticate(Config["email"], Config["password"]);
                if (result.AccessToken != null)
                {
                    Config["accesstoken"] = result.AccessToken;
                    Config.Providers.First().Set("accesstoken", result.AccessToken);

                    Debug.WriteLine(result.AccessToken); // Write to Debug window so it can be manually copy/pasted in appsettings.json for now
                }
            }
        }
    }
}
