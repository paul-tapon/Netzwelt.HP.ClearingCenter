using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Validators
{
    public class OrderAdviseValidator
    {
        private ClearingCenterDataContext db;

        public OrderAdviseValidator(ClearingCenterDataContext db)
        {
            this.db = db;
        }

        public IEnumerable<string> Validate(AdviseClearingCenterProductCommand args)
        {
            var errors = new List<string>();
            ValidateOrder(args.Order, errors);

            if (errors.Count == 0)
            {
                this.ValidateReferences(args, errors);
            }

            return errors;
        }

        private void ValidateReferences(AdviseClearingCenterProductCommand args, IList<string> errors)
        {
            int marketingProgramId = int.Parse(args.Order.MarketingProgramId);

            bool isLocalProgramActive = db.LocalPrograms.Any(x =>
                x.MarketingProgram.Id == marketingProgramId &&
                x.CountryIsoCode == args.Order.CountryIsoCode);

            if (!isLocalProgramActive)
            {
                errors.Add("Invalid marketing program: {0} - {1}".WithTokens(marketingProgramId, args.Order.CountryIsoCode));
                return;
            }
            
            int[] productGroups = (args.Order.Positions ?? new OrderPosition[0])
                .Select(x => x.ProductGroupId)
                .ToArray();

            bool isProductGroupValid = db.ProductGroups.Any(x => 
                x.IsActive &&
                productGroups.Contains(x.Id));

            if (!isProductGroupValid)
            {
                errors.Add("One or more invalid product group ID/s detected.");
                return;
            }   
        }

        private void ValidateOrder(Order order, List<string> errors)
        {
            if (order == null)
            {
                errors.Add("Order is required.");
                return;
            }

            if (order.CustomerData == null)
            {
                errors.Add("CustomerData is required.");
                return;
            }

            if (order.Positions == null || order.Positions.Length == 0)
            {
                errors.Add("There must be at least one position.");
                return;
            }

            // validate order attributes
            this.ReportErrors(order, errors);
            this.ReportErrors(order.CustomerData, errors, "CustomerData");

            // validate each position
            foreach (var position in order.Positions)
            {
                ValidatePosition(order, position, errors);
            }
        }

        private void ValidatePosition(Order order, OrderPosition position, List<string> errors)
        {
            if (position.PickupAddress == null)
            {
                errors.Add("Position.PickupAddress is required.");
                return;
            }

            if (position.Product == null)
            {
                errors.Add("Position.Product is required.");
            }

            this.ReportErrors(position, errors);
            this.ReportErrors(position.PickupAddress, errors, "Position.PickupAddress");
            this.ReportErrors(position.Product, errors);

            if (position.PickupDate == default(DateTime) || position.PickupDate < order.OrderDate)
            {
                errors.Add("Invalid pickup date for transport [{0}]".WithTokens(position.TransportNumber));
            }
        }

        private void ReportErrors(object instance, List<string> errors, string memberPath = null)
        {
            if (instance == null) return;
            var messages = instance.InvokeValidationAttributes();

            foreach (var msg in messages)
            {
                string prefix = memberPath.IsNullOrEmpty() ?
                    string.Empty : memberPath + ":";

                errors.Add("{0} {1}".WithTokens(prefix, msg.ErrorMessage));
            }
        }
    }
}
