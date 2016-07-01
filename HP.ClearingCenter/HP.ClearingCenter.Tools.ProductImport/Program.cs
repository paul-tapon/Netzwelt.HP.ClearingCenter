using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using HP.ClearingCenter.Application.Data;

namespace HP.ClearingCenter.Tools.ProductImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteCommand(args);

            Console.WriteLine("Done.");
#if DEBUG
            Console.ReadLine();
#endif
        }

        static void ExecuteCommand(string[] args)
        {
            string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles");

            if (args == null | args.Length == 0)
            {
                Console.WriteLine("Usage: HP.ClearingCenter.Tools.ProductImport [cc | b2i]");
                return;
            }

            switch (args[0])
            {
                case "cc":
                    new ClearingCenterProductImporter().ImportFrom(baseDirectory);
                    break;
                case "b2i":
                    new B2iProductImporter().ImportFrom(baseDirectory);
                    break;
            }
        }
    }
}
