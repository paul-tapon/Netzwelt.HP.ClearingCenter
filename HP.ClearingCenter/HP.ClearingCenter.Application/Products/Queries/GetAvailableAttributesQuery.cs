using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Queries
{
    public class GetAvailableAttributesQuery : IQuery<IEnumerable<CustomAttributeData>>
    {
        public IEnumerable<string> AssignedAttributes { get; set; }
    }
}
