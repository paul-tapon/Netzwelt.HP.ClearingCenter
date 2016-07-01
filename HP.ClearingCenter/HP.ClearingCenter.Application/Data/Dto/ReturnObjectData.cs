using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class ReturnObjectData
    {
        public int ProductGroupId { get; set; }
        public string ProductGroupExternalCode { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductGroupDescription { get; set; }

        public int CategoryID { get; set; }
        public string CategoryExternalCode { get; set; }
        public string CategoryName { get; set; }        

        public int ManufacturerId { get; set; }
        public string ManufacturerExternalCode { get; set; }
        public string ManufacturerName { get; set; }
        public int ProductId { get; set; }        
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }

        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string LengthUnit { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnit { get; set; }
    }
}
