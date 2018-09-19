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
        public async Task UnsuccessfulAuthenticationTest()
        {
                var result = await SenseApi.Authenticate("bob123456@test.com", "WhatIsGoingOn");
                Assert.IsFalse(result.Authorized);
        }

        [TestMethod]
        public void CheckAuthorizationResponseGetsSetTest()
        {
            Assert.IsTrue(SenseApi.AuthorizationResponse != null);
        }
    }
}
