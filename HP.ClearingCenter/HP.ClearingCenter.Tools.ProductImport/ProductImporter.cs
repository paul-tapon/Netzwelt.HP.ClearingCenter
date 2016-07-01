using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ProductImporter
    {
        private ClearingCenterDataContext db;
        private ConsoleLogger logger;

        public ProductImporter(ClearingCenterDataContext db, ConsoleLogger logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public virtual void Import(DataRow row)
        {
            var dto = ProductDto.CreateInstance(row);
            var existingProduct = db.Products.FirstOrDefault(x => x.ExternalProductId == dto.ExternalProductId);
            if (existingProduct != null)
            {
                this.logger.Warn("Product [{0}] with external ID [{1}] already exists. Skipping.".WithTokens(dto.ProductName, dto.ExternalProductId));
                return;
            }

            var manufacturer = this.FetchOrCreateManufacturer(dto);

            this.logger.Info("Importing product [{0}] with external ID [{1}]".WithTokens(dto.ProductName, dto.ExternalProductId));
            Product p = this.CreateProduct(dto, manufacturer);
            this.InsertCustomAttributes(p, row);
        }

        private Manufacturer FetchOrCreateManufacturer(ProductDto dto)
        {
            string externalCode = (dto.ManufacturerCode ?? string.Empty).Trim().ToUpperInvariant();
            var mfr = db.Manufacturers.FirstOrDefault(x => x.ExternalCode == externalCode);
            return mfr.Exists() ?
                mfr :
                db.Manufacturers.Add(new Manufacturer
                {
                    ExternalCode = externalCode,
                    Shortname = externalCode
                });

        }

        private void InsertCustomAttributes(Product p, DataRow row)
        {
            InsertAttribute("print_technology_ecosolvent", p, row);
            InsertAttribute("print_technology_solvent", p, row);
            InsertAttribute("print_technology_oil", p, row);
            InsertAttribute("print_technology_latex", p, row);
            InsertAttribute("print_technology_waterbased", p, row);
            InsertAttribute("paper_size_from", p, row);
            InsertAttribute("paper_size_to", p, row);
        }

        private void InsertAttribute(string customAttributeExternalCode, Product product, DataRow row)
        {
            if (!row.Table.Columns.Contains(customAttributeExternalCode)) return;

            var cav = db.ProductCustomAttributeValues.Add(new ProductCustomAttributeValue
            {
                Id = Guid.NewGuid(),
                Product = product,
                CustomAttributeExternalCode = customAttributeExternalCode,     
            });

            Func<string, bool> parseBool = input => input == "y" ? true : false;

            switch (customAttributeExternalCode)
            {
                case "print_technology_ecosolvent":                                        
                case "print_technology_solvent":
                case "print_technology_oil":
                case "print_technology_latex":
                case "print_technology_waterbased":
                    cav.BooleanValue = parseBool(row[customAttributeExternalCode].ToString());
                    break;
                case "paper_size_from":
                case "paper_size_to":
                    if (row[customAttributeExternalCode].ToString() != "NULL")
                    {
                        cav.IntegerValue = Convert.ToInt32(row[customAttributeExternalCode]);
                    }

                    break;
            }
        }

        private Product CreateProduct(ProductDto dto, Manufacturer manufacturer)
        {
            var p = db.Products.Add(new Product
            {
                ExternalProductId = dto.ExternalProductId,
                ShortName = (dto.ProductName ?? string.Empty).Trim(),
                Manufacturer = manufacturer,
                CreatedAt = DateTime.UtcNow.Date,
                CreatedBy = "system",
                IsActive = true,
            });

            return p;
        }
                
        class ProductDto
        {
            public static ProductDto CreateInstance(DataRow row)
            {
                return new ProductDto
                {
                    ExternalProductId = Convert.ToInt32(row.Field<double>("ExternalProductId")),
                    ProductName = row["ProductShortName"].ToString(),
                    ManufacturerCode = row["Manufacturer"].ToString()
                };
            }

            public string ManufacturerCode { get; set; }
            public string ProductName { get; set; }
            public int? ExternalProductId { get; set; }
        }
    }
}
