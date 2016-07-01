using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Queries
{
    public class SearchManufacturersQuery : IQuery<IEnumerable<ManufacturerData>>
    {
        public SearchManufacturersQuery()
        {
            this.MaxRows = 100;
        }
        
        public int? ManufacturerId { get; set; }

        public string Term { get; set; }

        public int MaxRows { get; set; }
        public int Page { get; set; }
    }
}
