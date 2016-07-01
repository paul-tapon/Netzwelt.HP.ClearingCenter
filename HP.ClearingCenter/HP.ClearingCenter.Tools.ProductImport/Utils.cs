using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public static class Utils
    {
        public static IEnumerable<string> GetDataFileList(string filePath)
        {
            foreach (var fileName in Directory.EnumerateFiles(filePath, "*.xlsx"))
            {
                yield return Path.Combine(filePath, fileName);
            }
        }
    }
}
