
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-维权订单详情(ec/rights/getRightsOrderDetail)
    /// </summary>
    public class SearchRightsOrderDetailResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }

        public class Data
        {
            public RightsInfo rightsInfo { get; set; }
        }

        public class RightsInfo
        {
            public long storeId { get; set; }
            public decimal refundAmount { get; set; }
            public string deliveryCompanyCode { get; set; }
            public string deliveryNo { get; set; }
            public long skuId { get; set; }
        }
    }

}