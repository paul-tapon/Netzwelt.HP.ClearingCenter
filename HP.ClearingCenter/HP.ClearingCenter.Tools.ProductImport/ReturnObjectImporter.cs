using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ReturnObjectImporter
    {
        private ClearingCenterDataContext db;
        private ConsoleLogger logger;

        public ReturnObjectImporter(ClearingCenterDataContext db, ConsoleLogger logger)
        {
            // TODO: Complete member initialization
            this.db = db;
            this.logger = logger;
        }

        public virtual void Import(DataRow row)
        {
            ReturnObjectMappingDto dto = ReturnObjectMappingDto.CreateInstance(row);

            Product ccProduct = this.GetClearingCenterProduct(dto);
            if (ccProduct == null)
            {
                this.logger.Warn("Clearing center product with external ID [{0}] does not exist!".WithTokens(dto.ExternalProductId));
                return;
            }

            this.ImportReturnObject(ccProduct, dto);

        }

        private void ImportReturnObject(Product ccProduct, ReturnObjectMappingDto dto)
        {
            var setupScheme = new SetupSchemeDataAdapter("WMCF_SetupScheme");

            if (setupScheme.IsReturnObjectGroupValid(dto.ReturnObjectGroupId))
            {
                // if cc_product.Id is not in B2i.T_ReturnObject, insert it.
                var ro = this.FetchOrCreateReturnObject(ccProduct, dto, setupScheme);

                // if return object is not yet assigned to product group, assign it.
                setupScheme.MapGroupAssignment(ro, dto);
            }
            else
            {
                this.logger.Warn("Invalid ReturnObjectGroupId detected: " + dto.ReturnObjectGroupId);
            }
        }

        private ReturnObjectData FetchOrCreateReturnObject(Product ccProduct, ReturnObjectMappingDto dto, SetupSchemeDataAdapter setupScheme)
        {
            var ro = setupScheme.GetReturnObject(ccProduct.Id);
            return ro ?? setupScheme.InsertReturnObject(new ReturnObjectData
            {
                ReturnObjectTypeId = 1,
                CC_ManufacturerId = ccProduct.Manufacturer.Id,
                CC_ProductId = ccProduct.Id,
                CC_Description = ccProduct.ShortName,
                CC_ManufacturerDescription = ccProduct.Manufacturer.Shortname
            });
        }

        private Product GetClearingCenterProduct(ReturnObjectMappingDto dto)
        {
            return this.db.Products
                .Include(x => x.Manufacturer)
                .FirstOrDefault(x => x.ExternalProductId == dto.ExternalProductId);
        }
    }
}
