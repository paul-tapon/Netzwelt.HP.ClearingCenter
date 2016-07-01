using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class StatusData
    {
        public bool IsSuccessful { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public DateTime Datestamp { get; set; }
        public string UpdatedBy { get; set; }
        public string Remarks { get; set; }
    }
}
