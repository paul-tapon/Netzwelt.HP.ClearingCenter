using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class ManufacturerData
    {
        public int? Id { get; set; }

        public string ExternalCode { get; set; }

        public string Shortname { get; set; }

        public string Description { get; set; }
    }
}
