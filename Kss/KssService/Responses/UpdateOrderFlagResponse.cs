
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-更新订单标记(ec/order/updateOrderFlag)
    /// </summary>
    public class UpdateOrderFlagResponse : BaseResponse
    {
        //public Code code { get; set; }
        public GoodsData data { get; set; }
    }

}