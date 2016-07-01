using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Tests.HP.ClearingCenter.WebServices;

namespace HP.ClearingCenter.Tests.WebServices
{
    [TestClass]
    public class AdviseClearingCenterProductWSTests
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
            var response = service.AdviseClearingCenterProduct(new AdviseClearingCenterProductRequest
            {
                Order = new Order
                {
                    MarketingProgramId = "1134",
                    CountryIsoCode = "DE",
                    OfferId = Guid.NewGuid().ToString(),
                    QuoteNumber = Guid.NewGuid().ToString(),
                    OrderDate = DateTime.UtcNow.Date,
                    CustomerData = new ContactAddress
                    {
                        CustomerName = "Tuschek, Greenova - Account",
                        CompanyName = "tt",
                        StreetAddress01 = "kartner",
                        City = "stuttgart",
                        PostalCode = "70469",
                        Phone = "12345",
                        EmailAddress = "tuschek@greenova.de"
                    },
                    Positions = new OrderPosition[] {
                        new OrderPosition {
                            TransportNumber = Guid.NewGuid().ToString(),
                            ProductGroupId = 1,
                            Quantity = 1,
                            TransactionDetailId = "1",
                            Product = new Product {
                                Manufacturer = "BRUNNING",
                                Model = "TestAndere",                                
                            },
                            PickupAddress = new ContactAddress {
                                CustomerName = "Tuschek, Greenova",
                                CompanyName = "tt",
                                StreetAddress01 = "kartner",
                                City = "stuttgart",
                                PostalCode = "70469",
                                Phone = "12345",
                                EmailAddress = "tuschek@greenova.de"
                            },
                            PickupDate = DateTime.UtcNow.Date.AddDays(3)
                        }
                    },
                }
            });

            Assert.IsTrue(response.IsSuccessful);
        }
    }
}
