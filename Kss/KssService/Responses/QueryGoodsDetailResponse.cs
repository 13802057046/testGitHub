
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-获取商品详情(ec/retailGoods/queryGoodsDetail)
    /// </summary>
    public class QueryGoodsDetailResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public Goods goods { get; set; }
        }
        public class Goods
        {
            public long goodsId { get; set; }
            public List<string> goodsImageUrl { get; set; }
        }

    }

}