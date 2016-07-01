using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class DataExtensions
    {
        public static T ReadData<T>(this IDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));

            if (value is DBNull) return default(T);

            return (T)reader.GetValue(reader.GetOrdinal(columnName));
        }
    }
}
