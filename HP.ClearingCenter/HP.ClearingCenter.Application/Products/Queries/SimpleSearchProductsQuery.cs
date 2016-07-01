using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Queries
{
    public class SimpleSearchProductsQuery : IQuery<IEnumerable<ProductData>>
    {
        public const int DEFAULT_MAX_ITEM_COUNT = 15;

        public SimpleSearchProductsQuery()
        {
            this.MaxItemCount = DEFAULT_MAX_ITEM_COUNT;
        }

        public int? ProductId { get; set; }

        public string Term { get; set; }

        public int MaxItemCount { get; set; }
        public int Page { get; set; }
    }
}
