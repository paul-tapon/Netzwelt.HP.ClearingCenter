using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class ProductData
    {
        public int? ProductId { get; set; }

        public int ManufacturerId { get; set; }

        public string Manufacturer { get; set; }

        public string ProductName { get; set; }

        public string ProductNumber { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public string LengthUnit { get; set; }

        public decimal? Weight { get; set; }

        public string WeightUnit { get; set; }

        public int? YearOfConstruction { get; set; }

        public string OriginCountryIsoCode { get; set; }

        public bool IsActive { get; set; }

        public string FullProductName 
        {
            get
            {
                string suffix = string.Empty;

                if (!this.ProductNumber.IsNullOrEmpty())
                {
                    suffix = " ({0}) ".WithTokens(this.ProductNumber);
                }

                return "{0} {1}{2}".WithTokens(this.Manufacturer, this.ProductName, suffix);
            }
        }

        public string GetDisplayText()
        {
            return this.FullProductName;
        }

        public override string ToString()
        {
            return this.GetDisplayText();
        }
    }
}
