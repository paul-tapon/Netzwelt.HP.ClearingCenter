using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryHandlers
{
    public class TransactionTransportQueryHandler : IQueryHandler<TransactionTransportQuery, IEnumerable<TransactionTransportData>>
    {
        public IEnumerable<TransactionTransportData> Retrieve(TransactionTransportQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                string input = (query.Input ?? string.Empty).Trim();

                return db.TransactionDetails
                    .Where(x => x.QuoteNumber == input || x.TransportNumber == input)
                    .Select(x => new TransactionTransportData
                    {
                        ProcessType = x.TransactionHeader.LocalProgram.MarketingProgram.ClearingProcessType.ShortName,
                        ProgramNumber = x.TransactionHeader.LocalProgram.MarketingProgram.Id,
                        ProgramName = x.TransactionHeader.LocalProgram.MarketingProgram.ShortName,
                        ProgramType = x.TransactionHeader.LocalProgram.MarketingProgram.MarketingProgramType.ShortName,
                        
                        QuoteNumber = x.QuoteNumber,
                        TransportNumber = x.TransportNumber,

                        PickupDate = x.PickupDate,
                        UnitsAdvised = x.UnitsAdvised,
                        ProductAdvised = new ProductData {
                            Manufacturer = x.ManufacturerNameAdvised,
                            ProductId = x.ProductId,
                            ProductNumber = x.ProductNumberAdvised,
                            ProductName = x.ProductNameAdvised,
                        },
                        
                        ReceivingStatus = new ProcessStatus { Source = x.ReceivingStatus },
                        ReceivingDate = x.ReceivingDate,
                        ReceivedBy = x.ReceivedBy,
                        UnitsReceived = x.UnitsReceived,
                        ReceivingRemarks = x.ReceivingRemarks,
                        IsReceivingLocked = x.ReceivingDate != null && x.ReceivingConfirmationDate != null,
                        
                        ClearingDate = x.ClearingDate,
                        ClearedBy = x.ClearedBy,
                        ClearingRemarks = x.ClearingRemarks,
                        ClearingStatus = new ProcessStatus { Source = x.ClearingStatus },
                        ProductCleared = new ProductData {
                            Manufacturer = x.ManufacturerNameReceived,
                            ProductId = x.ProductId,
                            ProductNumber = x.ProductNumberReceived,
                            ProductName = x.ProductNameReceived,
                        },
                        ForwardingInstruction = x.ForwardingInstruction,
                        IsClearingEnabled = x.TransactionHeader.LocalProgram.MarketingProgram.ClearingProcessType.IsDetailedClearingEnabled && x.ReceivingDate != null && x.ReceivingStatus != null && !x.ReceivingStatus.IsError,
                        IsClearingLocked = x.ClearingDate != null && x.ClearingConfirmationDate != null
                    })
                   .ToList();
            }
        }

        class ProcessStatus : IStatusCode
        {
            public IStatusCode Source 
            {
                set
                {
                    IStatusCode src = value ?? new ProcessStatus { ExternalCode = "0000", ShortName = "New", TranslatorShortcut = "cc_status_new" };
                    this.ExternalCode = src.ExternalCode;
                    this.ShortName = src.ShortName;
                    this.TranslatorShortcut = src.TranslatorShortcut;
                }
            }
            public string ExternalCode { get; private set; }

            public string ShortName { get; private set; }

            public string TranslatorShortcut { get; private set; }
        }
    }
}
