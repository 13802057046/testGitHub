using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-更新商品(ec/goods/updateGoods)
    /// </summary>
    public class UpdateGoodsRequest //: BaseRequest
    {
        public PendingUpdateGoodsVo goods { get; set; }
    }

    public class PendingUpdateGoodsVo
    {
        public long goodsId { get; set; }
        public string title { get; set; }
        public int isMultiSku { get; set; }
        public List<string> goodsImageUrl { get; set; }
        public string goodsDesc { get; set; }
        public int initialSales { get; set; }
        public int deductStockType { get; set; }
        public int isPutAway { get; set; }
        public int Sort { get; set; }
        public long categoryId { get; set; }
        //public List<PendingUpdateGoodsAttributeVo> selectedGoodsAttrList { get; set; }
        //public List<PendingUpdateGoodsAttributeVo> selectedSaleAttrList { get; set; }
        public PendingB2CGoodsVo b2cGoods { get; set; }
        public List<PendingSkuVo> skuList { get; set; }
    }

    //public class PendingUpdateGoodsAttributeVo
    //{
    //    public List<PendingUpdateGoodsAttributeValueVo> attrValueList { get; set; }
    //    public string attributeId { get; set; }
    //}

    //public class PendingUpdateGoodsAttributeValueVo
    //{
    //    public long key { get; set; }
    //}

    //public class PendingUpdateB2CGoodsVo
    //{
    //    public long freightTemplateId { get; set; }
    //    public List <long> deliveryTypeIdList { get; set; }
    //    public int b2cGoodsType { get; set; }
    //}

    //public class PendingUpdateSkuVo
    //{
    //    public long? skuId { get; set; }
    //    public string outerSkuCode { get; set; }
    //    public string imageUrl { get; set; }
    //    public decimal salePrice { get; set; }
    //    public decimal originalPrice { get; set; }
    //    public decimal costPrice { get; set; }
    //    public int editStockNum { get; set; }
    //    public PendingUpdateB2CSkuVo b2cSku { get; set; }
    //}

    //public class PendingUpdateB2CSkuVo
    //{
    //    public decimal weight { get; set; }
    //    public decimal volume { get; set; }
    //}

}