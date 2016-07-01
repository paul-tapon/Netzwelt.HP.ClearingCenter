using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
using System.Globalization;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    using HP.ClearingCenter.Application.TransactionTransports.Entities;

    public static partial class Data
    {
        public static void Seed(this ClearingCenterDataContext db)
        {
            CustomAttributes(db);
            db.SaveChanges();

            Products(db);
            db.SaveChanges();

            ProductGroups(db);

            TransactionTransports(db);
            db.SaveChanges();

            Users(db);
        }
    }
}
