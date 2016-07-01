using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryHandlers
{
    public class GetClearedProductsQueryHandler : IQueryHandler<GetClearedProductsQuery, TransportProcessData[]>
    {
        public TransportProcessData[] Retrieve(GetClearedProductsQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<TransactionDetail> transactionDetails = this.GetTransactionDetails(query, db);

                var resultSet = transactionDetails
                    .Where(x => 
                        x.TransactionHeader.LocalProgram.MarketingProgramId == query.MarketingProgramId &&
                        x.TransactionHeader.LocalProgram.CountryIsoCode == query.CountryIsoCode &&
                        x.ReceivingDate != null &&
                        (x.ReceivingStatus != null && x.ReceivingStatus.IsError == false) &&
                        x.ReceivingConfirmationDate != null &&
                        x.ClearingDate != null &&
                        x.ClearingStatus != null &&
                        x.ClearingConfirmationDate == null);

                if (query.StartDate.HasValue)
                {
                    resultSet = resultSet.Where(x =>
                        x.ClearingDate >= query.StartDate);
                }

                return resultSet
                    .Take(query.MaxRows)
                    .Select(x => new TransportProcessData
                    {
                        MarketingProgramId = x.TransactionHeader.LocalProgram.MarketingProgramId,
                        CountryIsoCode = x.TransactionHeader.LocalProgram.CountryIsoCode,
                        OfferId = x.TransactionHeader.OfferId,
                        QuoteNumber = x.TransactionHeader.QuoteNumber,
                        TransportNumber = x.TransportNumber,

                        ManufacturerName = x.ManufacturerNameReceived,
                        ProductNumber = x.ProductNumberReceived,
                        ProductName = x.ProductNameReceived,
                        SerialNumber = x.SerialNumber,
                        Quantity = x.UnitsAdvised,

                        ProductGroup = new ProductGroupData {
                            Id = x.ProductGroupId,
                            ExternalCode = x.ProductGroupExternalCode,
                            Description = x.ProductGroupDescription
                        },

                        ForwardingInstruction = x.ForwardingInstruction != null ? x.ForwardingInstruction.ExternalCode : null,

                        ReceivingStatus = new StatusData
                        {
                            IsSuccessful = x.ReceivingStatus != null ? !x.ReceivingStatus.IsError : false,
                            StatusCode = x.ReceivingStatus == null ? "9999" : x.ReceivingStatus.ExternalCode,
                            StatusDescription = x.ReceivingStatus == null ? "Unknown" : x.ReceivingStatus.ShortName,
                            Datestamp = (DateTime)x.ReceivingDate,
                            UpdatedBy = x.ReceivedBy,
                            Remarks = x.ReceivingRemarks
                        },

                        IsReceivingOk = x.ReceivingStatus != null ? !x.ReceivingStatus.IsError : false,
                        ReceivingDate = x.ReceivingDate,
                        ReceivedBy = x.ReceivedBy,
                        ReceivingRemarks = x.ReceivingRemarks,
                        ProblemCodeReceiving = x.ReceivingStatus != null ? x.ReceivingStatus.ExternalCode : null,

                        ClearingStatus = new StatusData
                        {
                            IsSuccessful = x.ClearingStatus != null ? !x.ClearingStatus.IsError : false,
                            StatusCode = x.ClearingStatus == null ? "9999" : x.ClearingStatus.ExternalCode,
                            StatusDescription = x.ClearingStatus == null ? "Unknown" : x.ClearingStatus.ShortName,
                            Datestamp = (DateTime)x.ClearingDate,
                            UpdatedBy = x.ClearedBy,
                            Remarks = x.ClearingRemarks
                        },

                        IsClearingOk = x.ClearingStatus != null ? !x.ClearingStatus.IsError : false,
                        ClearingDate = x.ClearingDate,
                        ClearedBy = x.ClearedBy,
                        ClearingRemarks = x.ClearingRemarks,
                        ProblemCodeClearing = x.ClearingStatus != null ? x.ClearingStatus.ExternalCode : null
                    })
                    .OrderBy(x => x.QuoteNumber)
                    .ToArray();
            }
        }

        private IQueryable<TransactionDetail> GetTransactionDetails(GetClearedProductsQuery args, ClearingCenterDataContext db)
        {
            var transactionDetails = db.TransactionHeaders
                .Where(x =>
                    x.LocalProgram.MarketingProgramId == args.MarketingProgramId &&
                    x.LocalProgram.CountryIsoCode == args.CountryIsoCode &&
                    !x.Details.Any(td => td.ReceivingDate == null) &&
                    !x.Details.Any(td => td.ClearingDate == null))
                .SelectMany(x => x.Details);

            return transactionDetails;
        }
    }
}
