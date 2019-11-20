
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-添加商品
    /// </summary>
    public class UpdateGoodsResponse : BaseResponse
    {
        //public Code code { get; set; }
        public GoodsData data { get; set; }
    }

}