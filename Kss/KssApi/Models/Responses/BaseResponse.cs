using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KssApi.Models.Responses
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}