using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Application.TransactionTransports.CommandHandlers;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.FrontEnd.Infrastructure.Services;

namespace HP.ClearingCenter.Tests.WebServices
{   
    [TestClass]
    public class AdviseClearingCenterProductTests
    {
        ContactAddress contactAddress = new ContactAddress
        {
            CompanyName = "company",
            CustomerName = "customer",
            StreetAddress01 = "street01",
            StreetAddress02 = "street02",
            StateOrProvince = "stateprovince",
            City = "city",
            EmailAddress = "ma@pa.co",
            Fax = "123455",
            Phone = "877812378",
            PostalCode = "ABC4 123J",
        };

        Product product = new Product
        {
            Manufacturer = "HP",
            Model = "Deskjet DJ1234",
            PartNumber = "12DJ499",
            CountryOfOrigin = "ph",
        };

        [TestMethod]
        public void ValidValuesTest()
        {
            var request = new AdviseClearingCenterProductCommand
            {
                Order = new Order
                {
                    CountryIsoCode = "de",
                    MarketingProgramId = "91000",                    
                    OrderDate = DateTime.UtcNow.Date.AddDays(30),
                    OfferId = "IDE12345678-90",
                    QuoteNumber = "QE1234567-89" + DateTime.Now.GetHashCode(),
                    CustomerData = contactAddress,                    
                    Positions = new OrderPosition[] {
                        new OrderPosition { 
                            ExpectedArrivalDate = DateTime.UtcNow.Date.AddDays(10),
                            PickupAddress = contactAddress,
                            PickupDate = DateTime.UtcNow.Date,
                            ProductGroupId = 1,
                            TransportNumber = "TDE" + DateTime.UtcNow.AddMilliseconds(235).Ticks.GetHashCode(),
                            Quantity = 1,
                            TransactionDetailId = Guid.NewGuid().ToString(),
                            Product = product
                        },
                    }
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test"
            };

            var handler = new AdviseClearingCenterProductCommandHandler();
            var context = new CommandContext<AdviseClearingCenterProductCommand>(handler, new NullValidationProvider());

            var result = context.Execute(request);

            Assert.IsTrue(result.IsSuccessful());
        }

        [TestMethod]
        public void InvalidOrderTest()
        {
            var handler = new AdviseClearingCenterProductCommandHandler();
            var context = new CommandContext<AdviseClearingCenterProductCommand>(handler, new NullValidationProvider());

            var request = new AdviseClearingCenterProductCommand
            {
                Order = new Order
                {
                    CountryIsoCode = "de",
                    MarketingProgramId = "91000",
                    OrderDate = DateTime.UtcNow.Date.AddDays(30),
                    CustomerData = contactAddress,
                    Positions = new OrderPosition[] {
                        new OrderPosition { 
                            ExpectedArrivalDate = DateTime.UtcNow.Date.AddDays(10),
                            PickupAddress = contactAddress,
                            PickupDate = DateTime.UtcNow.Date,
                            ProductGroupId = 1,
                            TransportNumber = "TDE" + DateTime.UtcNow.AddMilliseconds(235).Ticks.GetHashCode(),
                            Quantity = 1,
                            TransactionDetailId = Guid.NewGuid().ToString(),
                            Product = product
                        },
                    }
                },
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "test"
            };
            
            var result = context.Execute(request);

            Assert.IsFalse(result.IsSuccessful());
        }
    }
}
