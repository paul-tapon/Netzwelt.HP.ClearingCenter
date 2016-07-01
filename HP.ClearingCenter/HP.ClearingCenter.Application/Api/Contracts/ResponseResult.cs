using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Contracts
{
    [Serializable]
    public class ResponseResult
    {
        public ResponseResult()
        {
            this.ErrorMessages = new string[0];
            this.IsSuccessful = true;
        }

        public ResponseResult(string[] errors)
        {
            this.ErrorMessages = errors;
            this.IsSuccessful = false;
        }

        public bool IsSuccessful { get; set; }

        public string[] ErrorMessages { get; set; }
    }
}
