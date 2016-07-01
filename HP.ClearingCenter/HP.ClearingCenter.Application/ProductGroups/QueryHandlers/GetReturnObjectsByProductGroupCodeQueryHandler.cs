using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System.Data.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Application.Products.Entities;
using System.Data;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Builders;

namespace HP.ClearingCenter.Application.ProductGroups.QueryHandlers
{
    public class GetReturnObjectsByProductGroupCodeQueryHandler : IQueryHandler<GetReturnObjectsByProductGroupCodeQuery, IEnumerable<ReturnObjectData>>
    {
        public IEnumerable<ReturnObjectData> Retrieve(GetReturnObjectsByProductGroupCodeQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var pg = this.FindProductGroup(query, db);
                if (pg.IsNull()) return Enumerable.Empty<ReturnObjectData>();

                var pgc = this.FindProductGroupCategories(query, db);
                if (!pgc.Any()) return Enumerable.Empty<ReturnObjectData>();

                return this.ExecuteQuery(pg, pgc, db, query);
            }
        }

        private ProductGroup FindProductGroup(GetReturnObjectsByProductGroupCodeQuery query, ClearingCenterDataContext db)
        {
            return db.ProductGroups
                    .FirstOrDefault(x =>
                        x.MarketingProgramId == query.MarketingProgramId &&
                        x.CountryIsoCode == query.CountryIsoCode &&
                        x.ExternalCode == query.ProductGroupExternalCode);
        }

        private IEnumerable<ProductGroupCategory> FindProductGroupCategories(GetReturnObjectsByProductGroupCodeQuery query, ClearingCenterDataContext db)
        {
            return db.ProductGroupCategories
                    .Include(x => x.ProductGroup)
                    .Include(x => x.Filters)
                    .Where(x =>
                        x.ProductGroup.MarketingProgramId == query.MarketingProgramId &&
                        x.ProductGroup.CountryIsoCode == query.CountryIsoCode &&
                        x.ProductGroup.ExternalCode == query.ProductGroupExternalCode)
                    .OrderBy(x => x.ProductGroup.Id)
                    .ToList();
        }

        private IEnumerable<ReturnObjectData> ExecuteQuery(ProductGroup pg, IEnumerable<ProductGroupCategory> pgc, ClearingCenterDataContext db, GetReturnObjectsByProductGroupCodeQuery query)
        {
            if (db.Database.Connection.State != ConnectionState.Open)
            {
                db.Database.Connection.Open();
            }

            using (var cmd = this.BuildCommand(pgc, db, query))
            using (var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                var results = new List<ReturnObjectData>();
                while (dr.Read())
                {
                    results.Add(new ReturnObjectData
                    {
                        ProductGroupId = pg.Id,
                        ProductGroupExternalCode = pg.ExternalCode,
                        ProductGroupName = pg.ShortName,
                        ProductGroupDescription = pg.Description,

                        CategoryID = dr.ReadData<int>("CategoryID"),
                        CategoryExternalCode = dr.ReadData<string>("CategoryExternalCode"),
                        CategoryName = dr.ReadData<string>("CategoryName"),

                        ManufacturerId = dr.ReadData<int>("ManufacturerId"),
                        ManufacturerExternalCode = dr.ReadData<string>("ManufacturerExternalCode"),
                        ManufacturerName = dr.ReadData<string>("ManufacturerName"),

                        ProductId = dr.ReadData<int>("ProductId"),
                        ProductName = dr.ReadData<string>("ProductName"),
                        ProductNumber = dr.ReadData<string>("ProductNumber"),

                        Length = dr.ReadData<decimal?>("Length"),
                        Width = dr.ReadData<decimal?>("Width"),
                        Height = dr.ReadData<decimal?>("Height"),
                        LengthUnit = dr.ReadData<string>("LengthUnit"),

                        Weight = dr.ReadData<decimal?>("Weight"),
                        WeightUnit = dr.ReadData<string>("WeightUnit")
                    });
                }

                return results;
            }
        }

        private IDbCommand BuildCommand(IEnumerable<ProductGroupCategory> pgc, ClearingCenterDataContext db, GetReturnObjectsByProductGroupCodeQuery query)
        {
            var sqlBuilder = new GetReturnObjectGroupsSqlBuilder(db);
            return sqlBuilder.BuildCommand(pgc, query);
        }
    }
}
