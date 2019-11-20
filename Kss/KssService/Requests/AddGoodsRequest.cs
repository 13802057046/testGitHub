using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-添加商品
    /// </summary>
    public class AddGoodsRequest
    {
        public PendingAddGoodsVo goods { get; set; }
    }

    public class PendingAddGoodsVo
    {
        public string title { get; set; }
        public string outerGoodsCode { get; set; }
        public int isMultiSku { get; set; }
        public List <string > goodsImageUrl { get; set; }
        public string goodsDesc { get; set; }
        public int initialSales { get; set; }
        public int deductStockType { get; set; }
        public int isPutAway { get; set; }
        public int Sort { get; set; }
        public long categoryId { get; set; }
        public PendingB2CGoodsVo b2cGoods { get; set; }
        public List<PendingSkuVo> skuList { get; set; }
    }

    public class PendingB2CGoodsVo
    {
        public long freightTemplateId { get; set; }
        public List<long> deliveryTypeIdList { get; set; }
        public int b2cGoodsType { get; set; }
    }

    public class PendingSkuVo
    {
        public long? skuId { get; set; }
        public string outerSkuCode { get; set; }
        public string imageUrl { get; set; }
        public decimal salePrice { get; set; }
        public decimal originalPrice { get; set; }
        public decimal costPrice { get; set; }
        public int editStockNum { get; set; }
        public PendingB2CSkuVo b2cSku { get; set; }
    }

    public class PendingB2CSkuVo
    {
        public decimal weight { get; set; }
        public decimal volume { get; set; }
    }
    
}
