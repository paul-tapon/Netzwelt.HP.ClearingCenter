using HP.ClearingCenter.Application.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class GetProductListResponse : IApiResult
    {
        public GetProductListResponse()
        {
            this.Transports = new TransportProcessData[0];
            this.Result = new ResponseResult();
        }

        public TransportProcessData[] Transports { get; set; } 
        
        public ResponseResult Result { get; set; }
    }
}
