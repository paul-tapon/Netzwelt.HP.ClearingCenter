using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Application.ProductGroups.QueryHandlers;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryHandlers
{
    public class ValidateClearingProductQueryHandler : IQueryHandler<ValidateClearingProductQuery, ValidateClearingProductResult>
    {
        public ValidateClearingProductResult Retrieve(ValidateClearingProductQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var result = new ValidateClearingProductResult();

                var transport = db.TransactionDetails
                    .Include(x => x.TransactionHeader)
                    .Include(x => x.TransactionHeader.LocalProgram)
                    .FirstOrDefault(x => x.TransportNumber == query.TransportNumber);

                if (transport == null) return result;

                result.IsValid = this.GetQualifiedReturnObjects(transport, query)
                    .Any();

                return result;
            }
        }

        protected virtual IEnumerable<ReturnObjectData> GetQualifiedReturnObjects(TransactionDetail transport, ValidateClearingProductQuery args)
        {
            return new GetReturnObjectsByProductGroupCodeQueryHandler()
                .Retrieve(new GetReturnObjectsByProductGroupCodeQuery
                {
                    MarketingProgramId = transport.TransactionHeader.LocalProgram.MarketingProgramId,
                    CountryIsoCode = transport.TransactionHeader.LocalProgram.CountryIsoCode,
                    ProductGroupExternalCode = transport.ProductGroupExternalCode,
                    ProductId = args.SelectedProductId
                });
        }
    }
}
