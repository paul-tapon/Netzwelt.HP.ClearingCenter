using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Entities;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    public class TransactionTransportsDataAdapter : DataContextAdapter
    {
        public TransactionTransportsDataAdapter(IDataContext db) : base(db) { }

        public virtual TransactionDetail FindSingleTransactionDetail(string transportNumber)
        {
            transportNumber.ShouldNotBeEmpty("transportNumber");

            var transactionDetail = base.DataContext.FindOne<TransactionDetail>(x => x.TransportNumber == transportNumber);

            Protect.AgainstInvalidOperation(
                transactionDetail.IsNull(),
                "TransactionDetail with transportid '{0}' cannot be found.",
                transportNumber);

            return transactionDetail;
        }

        public virtual StatusCode FindSingleStatusCode(string externalCode)
        {
            externalCode.ShouldNotBeEmpty("externalCode");

            var statusCode = base.DataContext.FindOne<StatusCode>(x => x.ExternalCode == externalCode);

            Protect.AgainstInvalidOperation(statusCode.IsNull(),
                "StatusCode with externalcode '{0}' cannot be found.",
                externalCode);

            return statusCode;
        }

        public virtual ForwardingInstruction FindSingleForwardingInstruction(string externalCode)
        {
            externalCode.ShouldNotBeEmpty("externalCode");

            var fw = base.DataContext.FindOne<ForwardingInstruction>(x => x.ExternalCode == externalCode);

            Protect.AgainstInvalidOperation(fw.IsNull(),
                "ForwardingInstruction with externalcode '{0}' cannot be found.",
                externalCode);

            return fw;
        }

        public virtual Product FindProductById(int productId)
        {
            return base.DataContext.FindOne<Product>(x => x.Id == productId);
        }
    }
}
