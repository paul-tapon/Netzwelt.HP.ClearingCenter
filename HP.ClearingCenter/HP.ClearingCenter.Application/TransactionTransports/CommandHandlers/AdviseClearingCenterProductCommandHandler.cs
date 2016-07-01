using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HP.ClearingCenter.Application.TransactionTransports.Validators;
using HP.ClearingCenter.Application.ProductGroups.Entities;

namespace HP.ClearingCenter.Application.TransactionTransports.CommandHandlers
{
    public class AdviseClearingCenterProductCommandHandler : ICommandHandler<AdviseClearingCenterProductCommand>
    {
        public void Execute(ICommandContext<AdviseClearingCenterProductCommand> context)
        {
            using (var db = new ClearingCenterDataContext())
            {
                try
                {
                    if (!IsOrderValid(context, db))
                    {
                        return;
                    }

                    TransactionHeader th = this.FetchOrCreateTransactionHeader(context.Args, db);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var err in ex.EntityValidationErrors)
                    foreach (var validationError in err.ValidationErrors)
                    {
                        context.ReportError(validationError.ErrorMessage);
                    }

                    Trace.WriteLine(ex.ToString());
                }
            }
        }

        private bool IsOrderValid(ICommandContext<AdviseClearingCenterProductCommand> context, ClearingCenterDataContext db)
        {
            bool isValid = true;

            IEnumerable<string> errors = new OrderAdviseValidator(db)
                .Validate(context.Args);

            foreach (var err in errors)
            {
                context.ReportError(err);
                isValid = false;
            }
            
            return isValid;
        }

        private TransactionHeader FetchOrCreateTransactionHeader(AdviseClearingCenterProductCommand args, ClearingCenterDataContext db)
        {
            var th = db.TransactionHeaders
                .Include(x => x.Details)
                .Where(x =>
                    x.OfferId == args.Order.OfferId ||
                    x.QuoteNumber == args.Order.QuoteNumber)
                .FirstOrDefault();

            if (th == null)
            {
                th = db.TransactionHeaders.Add(new Entities.TransactionHeader
                {   
                    OfferId = args.Order.OfferId,
                    QuoteNumber = args.Order.QuoteNumber,
                    ContactCompany = args.Order.CustomerData.CompanyName,
                    ContactCustomer = args.Order.CustomerData.CustomerName,
                    ContactAddress = this.MapCustomerAddress(args.Order.CustomerData),
                    OrderDate = args.Order.OrderDate,
                    CreatedBy = args.CreatedBy,
                    CreatedAt = args.CreatedAt,
                    LocalProgram = this.GetLocalProgram(int.Parse(args.Order.MarketingProgramId), args.Order.CountryIsoCode, db)
                });
            }

            HandlePositions(args, db, th);

            return th;
        }

        private void HandlePositions(AdviseClearingCenterProductCommand args, ClearingCenterDataContext db, TransactionHeader th)
        {
            RemoveObsoleteTransactionDetails(args, db, th);
            IDictionary<int, ProductGroup> productGroups = this.GetProductGroups(args.Order.Positions, db);

            foreach (var position in args.Order.Positions)
            {
                var td = th.Details.FirstOrDefault(x => x.TransportNumber == position.TransportNumber);
                if (td == null)
                {
                    td = db.TransactionDetails.Add(new TransactionDetail
                    {
                        TransactionHeader = th,
                        ExternalId = position.TransactionDetailId,
                        TransportNumber = position.TransportNumber,
                        CreatedAt = args.CreatedAt,
                        QuoteNumber = args.Order.QuoteNumber
                    });

                    th.Details.Add(td);
                }
                
                td.EstimatedArrivalDate = position.ExpectedArrivalDate;
                td.PickupAddress = this.MapCustomerAddress(position.PickupAddress);
                td.PickupDate = position.PickupDate;
                td.PickupCustomer = position.PickupAddress.CustomerName;
                td.PickupCompany = position.PickupAddress.CompanyName;
                td.ProductGroupExternalCode = productGroups[position.ProductGroupId].ExternalCode;
                td.ManufacturerNameAdvised = position.Product.Manufacturer;
                td.ProductNameAdvised = position.Product.Model;
                td.ProductNumberAdvised = position.Product.PartNumber;
                td.SerialNumber = position.Product.SerialNumber;
                td.UnitsAdvised = position.Quantity;

                this.SetProductGroupData(td, productGroups[position.ProductGroupId]);
            }
        }

        private void SetProductGroupData(TransactionDetail td, ProductGroup pg)
        {
            td.ProductGroupId = pg.Id;
            td.ProductGroupExternalCode = pg.ExternalCode;
            td.ProductGroupDescription = pg.Description ?? pg.ShortName;
        }

        private IDictionary<int, ProductGroup> GetProductGroups(OrderPosition[] positions, ClearingCenterDataContext db)
        {
            var productGroupIds = (positions ?? new OrderPosition[0]).Select(x => x.ProductGroupId);
            var results = new Dictionary<int, ProductGroup>();
            
            foreach (var pg in db.ProductGroups.Where(x => x.IsActive && productGroupIds.Contains(x.Id)))
            {
                results.Add(pg.Id, pg);
            }

            return results;
        }

        private static void RemoveObsoleteTransactionDetails(AdviseClearingCenterProductCommand args, ClearingCenterDataContext db, TransactionHeader th)
        {
            // delete existing details that are not in the request
            var transportNumbers = args.Order.Positions.Select(x => x.TransportNumber);
            var obsoleteDetails = th.Details
                .Where(x => !transportNumbers.Contains(x.TransportNumber))
                .ToList();

            foreach (var od in obsoleteDetails)
            {
                th.Details.Remove(od);
                db.TransactionDetails.Remove(od);
            }
        }

        private Address MapCustomerAddress(ContactAddress ca)
        {
            if (ca == null) return null;

            return new Address {
                StreetAddress01 = ca.StreetAddress01,
                StreetAddress02 = ca.StreetAddress02,
                City = ca.City,
                PostalCode = ca.PostalCode,
                StateOrProvince = ca.StateOrProvince,
                EmailAddress = ca.EmailAddress,
                Fax = ca.Fax,
                Phone = ca.Phone
            };
        }

        private LocalProgram GetLocalProgram(int marketingProgramId, string countryIsoCode, ClearingCenterDataContext db)
        {
            LocalProgram localProgram = db.LocalPrograms
                .FirstOrDefault(x =>
                    x.MarketingProgram.Id == marketingProgramId &&
                    x.CountryIsoCode == countryIsoCode);

            return localProgram;
        }
    }
}
