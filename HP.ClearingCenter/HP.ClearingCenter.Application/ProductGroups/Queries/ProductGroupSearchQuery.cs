using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Queries
{
    public class ProductGroupSearchQuery : IQuery<IEnumerable<ProductGroupData>>
    {
        public ProductGroupSearchQuery()
        {
            MaxRowCount = 100;
        }


        public int? Id { get; set; }

        public string Term { get; set; }
        public int Page { get; set; }

        public int MaxRowCount { get; set; }
    }
}
