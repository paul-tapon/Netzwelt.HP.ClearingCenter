using HP.ClearingCenter.Tests.HP.ClearingCenter.WebServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.ClearingCenter.Tests.WebServices
{
    [TestClass]
    public class ReceivedProductsTests
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
        public void GetReceivedProductsTest() 
        {
            var response = service.GetReceivedProducts(new GetProductListRequest
            {
                MarketingProgramId = "91000",
                CountryIsoCode = "de",                
            });

            Assert.IsTrue(response.Result.IsSuccessful);
        }
        
        [TestMethod]
        public void GetClearedProductsTest()
        {
            var response = service.GetClearedProducts(new GetProductListRequest
            {
                MarketingProgramId = "91000",
                CountryIsoCode = "de",
            });

            Assert.IsTrue(response.Result.IsSuccessful);
        }

        [TestMethod]
        public void ReceiveAndConfirmReceivedProductsTest()
        {
            var response = service.GetReceivedProducts(new GetProductListRequest
            {
                MarketingProgramId = "1134",
                CountryIsoCode = "de",
            });

            foreach (var item in response.Transports)
            {
                var confirmResponse = service.ConfirmReceivedProductsReception(new ConfirmProcessStatusReceptionRequest
                {
                    TransportIds = new string[] { item.TransportNumber }
                });

                Assert.IsTrue(confirmResponse.IsSuccessful);
            }
        }

        [TestMethod]
        public void ReceiveAndConfirmClearedProductsTest()
        {
            var response = service.GetClearedProducts(new GetProductListRequest
            {
                MarketingProgramId = "1134",
                CountryIsoCode = "de"
            });

            foreach (var item in response.Transports)
            {   
                var confirmResponse = service.ConfirmClearedProductsReception(new ConfirmProcessStatusReceptionRequest
                {
                    TransportIds = new string[] { item.TransportNumber }
                });

                Assert.IsTrue(confirmResponse.IsSuccessful);
            }
        }
    }
}
