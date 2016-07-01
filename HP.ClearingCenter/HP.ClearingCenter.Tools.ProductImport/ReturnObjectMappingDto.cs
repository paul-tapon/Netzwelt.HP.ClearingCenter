using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ReturnObjectMappingDto
    {
        public static ReturnObjectMappingDto CreateInstance(DataRow row)
        {
            return new ReturnObjectMappingDto
            {
                ReturnObjectGroupId = row["ReturnObjectGroupId"].ToString(),
                ProductGroupId = Convert.ToInt32(row.Field<double>("ProductGroupId")),
                ExternalProductId = Convert.ToInt32(row.Field<double>("ExternalProductId"))
            };
        }

        public string ReturnObjectGroupId { get; set; }

        public int ProductGroupId { get; set; }

        public int ExternalProductId { get; set; }
    }
}
