using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-查询订单详情(ec/order/queryOrderDetail)
    /// </summary>
    public class QueryOrderDetailRequest
    {
        public long orderNo { get; set; }
    }

}
