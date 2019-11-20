using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-更新订单标记(ec/order/updateOrderFlag)
    /// </summary>
    public class UpdateOrderFlagRequest
    {
        public int flagRank { get; set; }
        public List<string> orderNoList { get; set; }
    }

}
