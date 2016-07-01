using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class AdviseClearingCenterProductResponse : IApiResult
    {
        public AdviseClearingCenterProductResponse()
        {
            this.Result = new ResponseResult();
        }

        public ResponseResult Result { get; set; }
    }
}
