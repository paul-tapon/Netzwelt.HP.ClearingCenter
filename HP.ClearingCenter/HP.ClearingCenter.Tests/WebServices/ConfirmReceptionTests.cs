using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.FrontEnd.Infrastructure.Services;
using HP.ClearingCenter.Application.TransactionTransports.CommandHandlers;

namespace HP.ClearingCenter.Tests.WebServices
{
    [TestClass]
    public class ConfirmReceptionTests
    {
        [TestMethod]
        public void ConfirmReceivedProductsTest()
        {
            var processor = new ConfirmReceivedProductsReceptionCommandHandler();
            var context = new CommandContext<ConfirmReceivedProductsReceptionCommand>(processor, new NullValidationProvider());

            var result = context.Execute(new ConfirmReceivedProductsReceptionCommand
            {
                TransportIds = new string[] { "T12345678" },
                ReceivingConfirmationDate = new DateTime(2015, 11, 01),
                ReceivingConfirmationUsername = "api_user"

            });

            Assert.IsTrue(!result.Errors.Any());
        }

        [TestMethod]
        public void ConfirmClearedProductsTest()
        {
            var processor = new ConfirmClearedProductsReceptionCommandHandler();
            var context = new CommandContext<ConfirmClearedProductsReceptionCommand>(processor, new NullValidationProvider());

            var result = context.Execute(new ConfirmClearedProductsReceptionCommand
            {
                TransportIds = new string[] { "T12345678" },
                ClearingConfirmationDate = new DateTime(2015, 11, 01),
                ClearingConfirmationUsername = "api_user"
            });

            Assert.IsTrue(!result.Errors.Any());
        }
    }
}
