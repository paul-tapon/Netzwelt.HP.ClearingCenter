using HP.ClearingCenter.Application.Api.Data;
using HP.ClearingCenter.Application.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class AdviseClearingCenterProductRequest
    {
        public Order Order { get; set; }
    }
}
