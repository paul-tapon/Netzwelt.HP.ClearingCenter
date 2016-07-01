using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.ProductGroups.QueryHandlers.Builders;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using System.Diagnostics;
using System.Data;

namespace HP.ClearingCenter.Tests.ProductGroups
{
    [TestClass]
    public class GetReturnObjectGroupsSqlBuilderTests
    {
        [TestMethod]
        public void BuildCommandTest()
        {
            using (var db = new ClearingCenterDataContext())
            {
                var pgc = db.ProductGroupCategories
                    .Where(x => x.ProductGroup.Id == 2)
                    .ToList();

                var args = new GetReturnObjectsByProductGroupCodeQuery
                {
                    ProductId = 16,
                    MarketingProgramId = 1134,
                    CountryIsoCode = "de",
                    ProductGroupExternalCode = "latex_selection"
                };

                var builder = new GetReturnObjectGroupsSqlBuilder(db);
                IDbCommand result = builder.BuildCommand(pgc, args);

                Trace.WriteLine(result.CommandText);
                int count = 0;

                try
                {
                    
                    result.Connection.Open();
                    using (var dr = result.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            count++;
                        }
                    }
                }
                finally
                {
                    result.Connection.Close();
                }

                Assert.IsTrue(count > 0, "actual: " + count);
            }
        }

        [TestMethod]
        public void GetChildCategoryCodesTest()
        {
            using (var db = new ClearingCenterDataContext())
            {
                var results = GetReturnObjectGroupsSqlBuilder.GetChildCategoryCodes("devices_printers_industrial_plotter", db);
                Assert.IsTrue(results.Any());
            }
        }
    }
}
