using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SenseApiTests
{
    [TestClass]
    public class AuthenticationTests : TestBase
    {
        [TestMethod]
        public void SuccessfulAuthenticationTest()
        {
            Assert.IsTrue(SenseApi.AuthorizationResponse.Authorized);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task UnsuccessfulAuthenticationTest()
        {
                await SenseApi.Authenticate("bob123456@test.com", "WhatIsGoingOn");
        }

        [TestMethod]
        public void CheckAuthorizationResponseGetsSetTest()
        {
            Assert.IsTrue(SenseApi.AuthorizationResponse != null);
        }
    }
}
