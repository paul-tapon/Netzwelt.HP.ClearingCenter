using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class EnumerableExtensions
    {
        public static string ToDelimitedValues<T>(this IEnumerable<T> inputs, string delimiter = ",")
        {
            return string.Join(delimiter, inputs.Select(x => x.ToString()).ToArray());
        }
    }
}
