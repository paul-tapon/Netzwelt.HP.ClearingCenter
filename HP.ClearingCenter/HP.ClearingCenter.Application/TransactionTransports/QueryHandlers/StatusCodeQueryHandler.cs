using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.TransactionTransports.Entities;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.Application.TransactionTransports.QueryResults;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.QueryHandlers
{
    public class StatusCodeQueryHandler : IQueryHandler<StatusCodeQuery, IEnumerable<IStatusCode>>
    {
        public IEnumerable<IStatusCode> Retrieve(StatusCodeQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<StatusCode> criteria = db.StatusCodes;

                if (query.IsReceiving.HasValue)
                {
                    criteria = criteria.Where(x => x.IsReceiving == query.IsReceiving);
                }

                if (query.IsClearing.HasValue)
                {
                    criteria = criteria.Where(x => x.IsClearing == query.IsClearing);
                }

                if (!string.IsNullOrEmpty(query.ExternalCode))
                {
                    string inputText = query.ExternalCode.Trim();
                    criteria = criteria.Where(x => x.ExternalCode == inputText);
                }

                return criteria.ToList();
            }
        }
    }
}
