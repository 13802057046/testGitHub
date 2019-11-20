using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-订单发货(单个)(ec/order/deliveryOrder)
    /// </summary>
    public class DeliveryOrderRequest
    {
        public long orderNo { get; set; }
        public string deliveryNo { get; set; }
        public string deliveryCompanyCode { get; set; }
        public string deliveryCompanyName { get; set; }
    }

}
