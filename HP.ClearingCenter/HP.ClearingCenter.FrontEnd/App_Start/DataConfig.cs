using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.App_Start
{
    public static class DataConfig
    {
        public static void Initialize()
        {
#if DEBUG
            Database.SetInitializer(DbInitializer.CreateInstance<ClearingCenterDataContext>());

            using (var db = new ClearingCenterDataContext())
            {
                db.ApplicationUsers.Any(x => true == false);
            }
#endif
        }
    }
}