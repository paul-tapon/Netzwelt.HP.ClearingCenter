using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public static class DatabaseHelper
    {
        public static IDbConnection PrepareConnection(string connectionName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionName];
            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            var conn = factory.CreateConnection();

            // open the connection
            conn.ConnectionString = connectionString.ConnectionString;
            conn.Open();

            return conn;
        }
    }
}
