using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Commands
{
    public class AddOrUpdateProductCommand : ICommand
    {
        private string productNumber;
        
        public AddOrUpdateProductCommand()
        {
            this.IsActive = true;
        }

        public int ProductId { get; set; }

        [StringLength(128)]
        public string ProductNumber {
            get { return this.productNumber; }
            set
            {
                string val = null;
                if (!value.IsNullOrEmpty())
                {
                    val = value.Trim()
                             .Replace(' ', '_')
                             .ToUpperInvariant();
                }

                if (val.IsNullOrEmpty())
                {
                    val = null;
                }

                this.productNumber = val;
            }
        }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public int ManufacturerId { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        [StringLength(64)]
        public string LengthUnit { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(64)]
        public string WeightUnit { get; set; }
        
        public int? YearOfConstruction { get; set; }

        [StringLength(3)]
        public string OriginCountryIsoCode { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
    }
}
