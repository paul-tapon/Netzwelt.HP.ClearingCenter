﻿using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ClearingCenterProductImporter
    {
        public virtual void ImportFrom(string baseDirectory)
        {
            string filePath = Path.Combine(baseDirectory, "ClearingCenter");
            IEnumerable<string> dataFiles = Utils.GetDataFileList(filePath);
            foreach (var filename in dataFiles)
            {
                ImportFile(filename);
            }
        }

        private static void ImportFile(string filename)
        {
            using (var file = File.OpenRead(filename))
            using (var reader = Excel.ExcelReaderFactory.CreateOpenXmlReader(file))
            {
                reader.IsFirstRowAsColumnNames = true;
                var table = reader.AsDataSet().Tables[0];
                PerformImport(table);
            }
        }

        private static void PerformImport(DataTable table)
        {
            using (var db = new ClearingCenterDataContext())
            {
                var importer = new ProductImporter(db, new ConsoleLogger());

                foreach (DataRow row in table.Rows)
                {
                    importer.Import(row);
                    db.SaveChanges();
                }
            }
        }
    }
}
