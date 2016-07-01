using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Tools.ProductImport
{
    public class ConsoleLogger
    {
        public virtual void Info(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Warn(string message)
        {
            this.Info(message);
        }
    }
}
