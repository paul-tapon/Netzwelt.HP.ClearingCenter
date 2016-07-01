using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    public interface IApiResult
    {
        ResponseResult Result { get; set; }
    }
}
