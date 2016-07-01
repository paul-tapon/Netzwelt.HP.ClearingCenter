using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HP.ClearingCenter.Application.Data;

namespace HP.ClearingCenter.Tests.EntityFramework
{
    [TestClass]
    public class CustomAttributeTests
    {
        [TestMethod]
        public void GetCustomAttribute()
        {
            using (var db = new ClearingCenterDataContext())
            {
                var ca = db.CustomAttributes.Include(x => x.CustomAttributeDataType).FirstOrDefault();
                Assert.IsNotNull(ca.CustomAttributeDataType);
                Assert.IsTrue(ca.CustomAttributeDataType.Id != 0);
            }
        }
    }
}
