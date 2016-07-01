using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class SetupSchemeDataAdapter
    {
        private string connectionName;   

        public SetupSchemeDataAdapter(string connectionName)
        {
            this.connectionName = connectionName;
        }

        public virtual ReturnObjectData GetReturnObject(int ccProductId)
        {
            using (var db = DatabaseHelper.PrepareConnection(this.connectionName)) 
            {
                var results = db.Query<ReturnObjectData>(
                    " SELECT * FROM T_ReturnObject WHERE ReturnObjectTypeId = 1 AND CC_ProductId = @ccProductId AND CC_TechnologyId IS NULL ",
                    new { ccProductId });

                return results.FirstOrDefault();
            }
        }

        public virtual ReturnObjectData InsertReturnObject(ReturnObjectData ro)
        {
            using (var db = DatabaseHelper.PrepareConnection(this.connectionName))
            {
                ro.Id = Convert.ToInt32(db.ExecuteScalar(
                    " INSERT INTO T_ReturnObject(ReturnObjectTypeId, CC_CategoryId, CC_TechnologyId, CC_ManufacturerId, CC_ProductId, CC_Description, CC_ManufacturerDescription, CC_Length, CC_Height, CC_Width, CC_Weight, DefaultPorts) " +
                    " VALUES (@ReturnObjectTypeId, @CC_CategoryId, @CC_TechnologyId, @CC_ManufacturerId, @CC_ProductId, @CC_Description, @CC_ManufacturerDescription, 0, 0, 0, 0, 0) " +
                    " SELECT @@IDENTITY ",
                    ro));

                return ro;
            }
        }

        public virtual void MapGroupAssignment(ReturnObjectData ro, ReturnObjectMappingDto dto)
        {
            if (!this.IsObjectAssignedToGroup(ro, dto))
            {
                this.InsertReturnObjectGroupAssignment(ro, dto);
            }
        }

        public bool IsObjectAssignedToGroup(ReturnObjectData ro, ReturnObjectMappingDto dto)
        {
            using (var db = DatabaseHelper.PrepareConnection(this.connectionName))
            {
                int result = Convert.ToInt32(db.ExecuteScalar(
                    " SELECT COUNT(*) FROM T_ReturnObjectGroup_mn_ReturnObject WHERE ReturnObjectGroupId = @ReturnObjectGroupId AND ReturnObjectId = @ReturnObjectId ",
                    new { dto.ReturnObjectGroupId, ReturnObjectId = ro.Id }));

                
                return result > 0;
            }
        }

        public bool IsReturnObjectGroupValid(string returnObjectGroupId)
        {
            using (var db = DatabaseHelper.PrepareConnection(this.connectionName))
            {
                int result = Convert.ToInt32(db.ExecuteScalar(
                    " SELECT COUNT(*) FROM T_ReturnObjectGroup WHERE Id = @ReturnObjectGroupId ",
                    new { ReturnObjectGroupId = returnObjectGroupId }));

                return result > 0;
            }
        }

        public void InsertReturnObjectGroupAssignment(ReturnObjectData ro, ReturnObjectMappingDto dto)
        {
            using (var db = DatabaseHelper.PrepareConnection(this.connectionName))
            {
                db.Execute(
                    " INSERT INTO T_ReturnObjectGroup_mn_ReturnObject(ReturnObjectGroupId, ReturnObjectId, DoNotExpand, CC_ProductGroupId) " +
                    " VALUES(@ReturnObjectGroupId, @ReturnObjectId, 0, @CC_ProductGroupId) ",
                    new { dto.ReturnObjectGroupId, ReturnObjectId = ro.Id, CC_ProductGroupId = dto.ProductGroupId  });
            }
        }
    }
}
