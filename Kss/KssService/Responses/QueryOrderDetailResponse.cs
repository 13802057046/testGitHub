
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-查询订单详情(ec/order/queryOrderDetail)
    /// </summary>
    public class QueryOrderDetailResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public int deliveryPaymentAmount { get; set; }
            public DeliveryDetail deliveryDetail { get; set; }
        }

        public class DeliveryDetail
        {
            public LogisticsDeliveryDetail logisticsDeliveryDetail { get; set; }
        }

        public class LogisticsDeliveryDetail
        {
            public List<LogisticsOrderVo> logisticsOrderList { get; set; }
            public string receiverProvince { get; set; }
            public string receiverCity { get; set; }
            public string receiverCounty { get; set; }
            public string receiverAddress { get; set; }
            public string receiverName { get; set; }
            public string receiverMobile { get; set; }
            public string receiverZip { get; set; }
        }

        public class LogisticsOrderVo
        {
            public string deliveryCompanyCode { get; set; }
            public string deliveryNo { get; set; }
        }
    }

}