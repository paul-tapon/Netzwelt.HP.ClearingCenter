using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Tests.HP.ClearingCenter.WebServices;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;

namespace HP.ClearingCenter.Tests.WebServices
{
    [TestClass]
    public class EchoTest
    {
        MainInterface service = null;

        [TestInitialize]
        public void Initialize()
        {
            this.service = new MainInterface();
            var auth = new ApiAuthenticationToken
            {
                Username = "api_user",
                ApiKey = "VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIHRoZSBsYXp5IGRvZy4=",
            };

            service.ApiAuthenticationTokenValue = auth;
        }

        [TestMethod]
        public void ItWorksTest()
        {
            var response = service.Echo(new EchoRequest { Message = "Hello World" });
            Assert.IsTrue(response.IsSuccessful);
        }
    }
}
