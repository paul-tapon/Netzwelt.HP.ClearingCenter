using HP.ClearingCenter.Application.ProductGroups.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class CategoryFilterAttributeData
    {
        public Guid ProductFilterId { get; set; }
        public string AttributeShortCode { get; set; }
        public string AttributeShortName { get; set; }
        public string OperatorShortName { get; set; }
        public FilterOperator FilterOperator { get; set; }
        public string[] Values { get; set; }

        public string ValuesCsv
        {
            get
            {
                StringBuilder s = new StringBuilder();

                string[] vals = Values ?? new string[0];

                foreach (var v in vals)
                {
                    if (s.Length > 0)
                    {
                        s.Append(", ");
                    }

                    s.Append(v.Trim());
                }

                return s.ToString();
            }
        }

        public bool IsBinaryOperator 
        {
            get
            {
                return this.FilterOperator == ProductGroups.Entities.FilterOperator.Between;
            }
        }
    }
}
