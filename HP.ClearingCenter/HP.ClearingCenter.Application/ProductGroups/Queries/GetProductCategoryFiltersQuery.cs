using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Queries
{
    public class GetProductCategoryFiltersQuery : IQuery<IEnumerable<CategoryData>>
    {
        public int ProductGroupId { get; set; }
    }
}
