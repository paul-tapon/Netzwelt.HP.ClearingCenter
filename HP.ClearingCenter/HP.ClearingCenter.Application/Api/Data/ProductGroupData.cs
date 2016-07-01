using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class ProductGroupData
    {
        public int Id { get; set; }
        public string ExternalCode { get; set; }
        public string Description { get; set; }
    }
}
