
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-退货/退款商家确认收货接口(ec/rights/confirmReceivedRightsGoods)
    /// </summary>
    public class ConfirmReceivedRightsGoodsResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public string success { get; set; }
        }

    }

}