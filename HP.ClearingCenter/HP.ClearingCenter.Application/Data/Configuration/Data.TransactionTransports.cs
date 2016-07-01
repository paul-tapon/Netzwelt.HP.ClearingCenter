using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
using System.Globalization;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    using HP.ClearingCenter.Application.TransactionTransports.Entities;

    public static partial class Data
    {
        private static void TransactionTransports(HP.ClearingCenter.Application.Data.ClearingCenterDataContext context)
        {
            // marketing program types
            context.MarketingProgramTypes.AddOrUpdate(
                new MarketingProgramType { Id = (int)ProgramType.TradeIn, ExternalCode = "{0}.{1}".WithTokens(typeof(ProgramType).FullName, ProgramType.TradeIn.ToString()), ShortName = "Trade-In" },
                new MarketingProgramType { Id = (int)ProgramType.BuyAndTry, ExternalCode = "{0}.{1}".WithTokens(typeof(ProgramType).FullName, ProgramType.BuyAndTry.ToString()), ShortName = "Buy and Try" }
            );

            // clearing process types
            var clearingProcess = new ClearingProcessType { ExternalCode = "clearing", ShortName = "Receiving and Clearing", IsDetailedClearingEnabled = true };
            context.ClearingProcessTypes.AddOrUpdate(
                new ClearingProcessType { ExternalCode = "receiving", ShortName = "Low-level Receiving Only", IsDetailedClearingEnabled = false },
                clearingProcess
            );

            // clearing center
            context.ClearingCenters.AddOrUpdate(
                new ClearingCenter { Id = 0, ExternalCode = "GSG", ShortName = "GreeNova Services GmbH", CountryIsoCode = "de", LanguageIsoCode = "de" }
            );

            // forwarding instructions
            context.ForwardingInstructions.AddOrUpdate(
                new ForwardingInstruction { ExternalCode = "none", ShortName = "None" },
                new ForwardingInstruction { ExternalCode = "recycling", ShortName = "Recycling" },
                new ForwardingInstruction { ExternalCode = "remarketing", ShortName = "Remarketing" }
            );

            // status codes
            context.StatusCodes.AddOrUpdate(
                new StatusCode { ExternalCode = Domain.Default.SuccessfulClearingStatusCode, ShortName = "OK", IsClearing = true, IsReceiving = true, IsError = false },
                new StatusCode { ExternalCode = "0500", ShortName = "Unknown Fatal Error", IsClearing = true, IsReceiving = true, IsError = true }
            );

            // marketing program
            context.MarketingPrograms.AddOrUpdate(
                new MarketingProgram { Id = 91000, ShortName = "Trade-in", MarketingProgramTypeId = (int)ProgramType.TradeIn, ClearingProcessType = clearingProcess },
                new MarketingProgram { Id = 99900, ShortName = "Buy and Try", MarketingProgramTypeId = (int)ProgramType.BuyAndTry, ClearingProcessType = clearingProcess }
            );

            // local program
            DateTime termStart = DateTime.ParseExact("2015-11-01", "yyyy-MM-dd", CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
            var simpleTradeIn = new LocalProgram { MarketingProgramId = 91000, CountryIsoCode = "de", ShortName = "Simple Trade-In Promo", TermStartDate = termStart, TermEndDate = termStart.AddYears(100) };

            context.LocalPrograms.AddOrUpdate(
                simpleTradeIn
            );

            CreateTestOrders(simpleTradeIn, context);
        }

        private static void CreateTestOrders(LocalProgram simpleTradeIn, ClearingCenterDataContext context)
        {
            var testAddress = new Dto.Address
            {
                StreetAddress01 = "Street 01",
                StreetAddress02 = "Street 02",
                StateOrProvince = "District",
                City = "City",
                PostalCode = "123 456",
                Fax = "12345678",
                Phone = "123455667",
                EmailAddress = "email@address.co"
            };

            var t = new TransactionHeader
            {
                QuoteNumber = "Q12345678-90",
                OfferId = "EH123890123-91",
                ContactCompany = "Contact Company",
                ContactCustomer = "Contact Customer",
                CreatedAt = DateTime.UtcNow,
                OrderDate = DateTime.UtcNow.Date,
                CreatedBy = "system",
                LocalProgram = simpleTradeIn,
                ContactAddress = testAddress
            };

            context.TransactionHeaders.Add(t);
            context.TransactionDetails.Add(new TransactionDetail
            {
                TransportNumber = "T12345678",
                QuoteNumber = t.QuoteNumber,
                TransactionHeader = t,
                CreatedAt = DateTime.UtcNow,
                EstimatedArrivalDate = DateTime.UtcNow.AddDays(1).Date,
                PickupDate = DateTime.UtcNow.Date,
                PickupCompany = "Pickup Company",
                PickupCustomer = "Pickup Customer",
                PickupAddress = testAddress,
                ProductGroupExternalCode = "all_hp_products",
                ManufacturerNameAdvised = "HP",
                ProductNumberAdvised = "XXB1479",
                ProductNameAdvised = "Elitebook 1020x",
                UnitsAdvised = 2,
            });

            context.TransactionDetails.Add(new TransactionDetail
            {
                TransportNumber = "T87654321",
                QuoteNumber = t.QuoteNumber,
                TransactionHeader = t,
                CreatedAt = DateTime.UtcNow,
                EstimatedArrivalDate = DateTime.UtcNow.AddDays(1).Date,
                PickupDate = DateTime.UtcNow.Date,
                PickupCompany = "Pickup Company",
                PickupCustomer = "Pickup Customer",
                PickupAddress = testAddress,
                ProductGroupExternalCode = "all_hp_products",
                ManufacturerNameAdvised = "HP",
                ProductNumberAdvised = "LJA1123",
                ProductNameAdvised = "LaserJet 330p",
                UnitsAdvised = 2,
            });

            var t2 = new TransactionHeader
            {
                QuoteNumber = "Q22345678-69",
                OfferId = "EH333898123-99",
                ContactCompany = "Contact Company",
                ContactCustomer = "Contact Customer",
                CreatedAt = DateTime.UtcNow,
                OrderDate = DateTime.UtcNow.Date,
                CreatedBy = "system",
                LocalProgram = simpleTradeIn,
                ContactAddress = testAddress
            };

            context.TransactionHeaders.Add(t2);

            context.TransactionDetails.Add(new TransactionDetail
            {
                TransportNumber = "T33335669",
                QuoteNumber = t2.QuoteNumber,
                TransactionHeader = t2,
                CreatedAt = DateTime.UtcNow,
                EstimatedArrivalDate = DateTime.UtcNow.AddDays(1).Date,
                PickupDate = DateTime.UtcNow.Date,
                PickupCompany = "Pickup Company",
                PickupCustomer = "Pickup Customer",
                PickupAddress = testAddress,
                ProductGroupExternalCode = "all_hp_products",
                ManufacturerNameAdvised = "HP",
                ProductNumberAdvised = "SRV9837",
                ProductNameAdvised = "Proliant X110",
                UnitsAdvised = 1,
            });
        }
    }
}
