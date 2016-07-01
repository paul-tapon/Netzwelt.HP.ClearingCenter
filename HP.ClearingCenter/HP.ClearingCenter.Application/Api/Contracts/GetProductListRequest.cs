using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class GetProductListRequest
    {
        public string MarketingProgramId { get; set; }

        public string CountryIsoCode { get; set; }

        public DateTime? StartDate { get; set; }
    }
}
