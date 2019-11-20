
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-添加商品
    /// </summary>
    public class AddGoodsResponse : BaseResponse
    {
        //public Code code { get; set; }
        public GoodsData data { get; set; }
    }

    public class GoodsData
    {
        public string deletedGoodsIds { get; set; }
        public string result { get; set; }
        public string outerGoodsIdList { get; set; }
        public long goodsId { get; set; }
        public List<GoodsSku> skuList { get; set; }
        public string distributorId { get; set; }
        public string distributorResponse { get; set; }
    }

    public class GoodsSku
    {
        public string outerSkuCode { get; set; }
        public string title { get; set; }
        public string imageUrl { get; set; }
        public long skuId { get; set; }
        public string storeId { get; set; }
    }
}