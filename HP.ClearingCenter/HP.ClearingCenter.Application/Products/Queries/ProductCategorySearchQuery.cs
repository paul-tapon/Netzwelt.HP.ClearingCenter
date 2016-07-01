using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Queries
{
    public class ProductCategorySearchQuery : IQuery<IEnumerable<CategoryData>>
    {
        public ProductCategorySearchQuery()
        {
            this.MaxResults = 100;
            ExcludedCategoryIds = Enumerable.Empty<int>();
        }
        
        public int MaxResults { get; set; }
        
        public int? CategoryId { get; set; }
        
        public string Term { get; set; }


        public int Page { get; set; }

        public IEnumerable<int> ExcludedCategoryIds { get; set; }
    }
}
