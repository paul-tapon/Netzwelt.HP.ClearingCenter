using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryHandlers
{
    public class ForwardingInstructionsQueryHandler : IQueryHandler<ForwardingInstructionsQuery, IEnumerable<IForwardingInstruction>>
    {
        public IEnumerable<IForwardingInstruction> Retrieve(ForwardingInstructionsQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                return db.ForwardingInstructions
                    .ToList();
            }
        }
    }
}
