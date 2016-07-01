using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Queries
{
    public class GetCustomAttributeQuery : IQuery<IEnumerable<CustomAttributeData>>
    {
        public GetCustomAttributeQuery()
        {
            this.MaxRowCount = 100;
        }
        
        public int? CustomAttributeId { get; set; }

        public string ExternalCode { get; set; }

        public int MaxRowCount { get; set; }
        public int Page { get; set; }
    }
}
