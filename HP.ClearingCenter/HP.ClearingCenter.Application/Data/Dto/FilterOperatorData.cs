using HP.ClearingCenter.Application.ProductGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class FilterOperatorData
    {
        public FilterOperatorData(FilterOperator id)
        {
            this.Id = id;
            this.ShortName = id.ToString();
        }

        public FilterOperator Id { get; set; }
        public string ShortName { get; set; }
    }
}
