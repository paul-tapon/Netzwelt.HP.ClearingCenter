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
    public class GetReceivedProductsQueryHandler : IQueryHandler<GetReceivedProductsQuery, TransportProcessData[]>
    {
        public TransportProcessData[] Retrieve(GetReceivedProductsQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<TransactionDetail> transactionDetails = this.GetTransactionDetails(query, db);

                var resultSet = transactionDetails
                    .Where(x =>
                        x.TransactionHeader.LocalProgram.MarketingProgramId == query.MarketingProgramId &&
                        x.TransactionHeader.LocalProgram.CountryIsoCode == query.CountryIsoCode &&
                        x.ReceivingDate != null &&
                        x.ReceivingStatus != null && 
                        x.ReceivingConfirmationDate == null);

                if (query.StartDate.HasValue)
                {
                    resultSet = resultSet.Where(x =>
                        x.ReceivingDate >= query.StartDate);
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
                        Quantity = x.UnitsReceived != null ? (int)x.UnitsReceived : x.UnitsAdvised,

                        ProductGroup = new ProductGroupData
                        {
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

        private IQueryable<TransactionDetail> GetTransactionDetails(GetReceivedProductsQuery args, ClearingCenterDataContext db)
        {
            var transactionDetails = db.TransactionHeaders
                .Where(x =>
                    x.LocalProgram.MarketingProgramId == args.MarketingProgramId &&
                    x.LocalProgram.CountryIsoCode == args.CountryIsoCode &&
                    !x.Details.Any(td => td.ReceivingDate == null))
                .SelectMany(x => x.Details);

            return transactionDetails;
        }
    }
}
