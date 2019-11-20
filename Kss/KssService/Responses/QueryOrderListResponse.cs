
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-查询订单列表(ec/order/queryOrderList)
    /// </summary>
    public class QueryOrderListResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public List<PageListItem> pageList { get; set; }
            public int pageNum { get; set; }
            public int pageSize { get; set; }
            public int totalCount { get; set; }
        }

        public class PageListItem
        {
            public long orderNo { get; set; }
            public DateTime createTime { get; set; }
            public DateTime updateTime { get; set; }
            public long storeId { get; set; }
            public List<MerchantOrderItemVo> itemList { get; set; }

            //public long pid { get; set; }
            //public long wid { get; set; }
            //public string bizOrderId { get; set; }
            //public int bizType { get; set; }
            //public string buyerRemark { get; set; }
            //public int channelType { get; set; }
            //public string channelTypeName { get; set; }
            //public string confirmReceivedTime { get; set; }
            //public int deliveryAmount { get; set; }
            //public string deliveryOrderId { get; set; }
            //public string deliveryTime { get; set; }
            //public int deliveryType { get; set; }
            //public string deliveryTypeName { get; set; }
            //public int enableDelivery { get; set; }
            //public string expectDeliveryTime { get; set; }
            //public string flagContent { get; set; }
            //public string flagRank { get; set; }
            //public double goodsAmount { get; set; }
            //public GoodsPromotionInfo goodsPromotionInfo { get; set; }
            //public int orderStatus { get; set; }
            //public string orderStatusName { get; set; }
            //public double paymentAmount { get; set; }
            //public string paymentMethodName { get; set; }
            //public int paymentStatus { get; set; }
            //public string paymentTime { get; set; }
            //public int paymentType { get; set; }
            //public string paymentTypeName { get; set; }
            //public int processStoreId { get; set; }
            //public string processStoreTitle { get; set; }
            //public string receiverAddress { get; set; }
            //public string receiverArea { get; set; }
            //public string receiverCity { get; set; }
            //public string receiverCounty { get; set; }
            //public string receiverMobile { get; set; }
            //public string receiverName { get; set; }
            //public string receiverProvince { get; set; }
            //public string selfPickupSiteName { get; set; }
            //public string selfPickupStatus { get; set; }
            //public string storeTitle { get; set; }
            //public string subBizType { get; set; }
            //public string totalPoint { get; set; }
            //public string transferFailReason { get; set; }
            //public int transferStatus { get; set; }
            //public int transferType { get; set; }
            //public string userNickname { get; set; }
        }

        public class MerchantOrderItemVo
        {
            public decimal totalAmount { get; set; }
            public decimal paymentAmount { get; set; }
            public long goodsId { get; set; }
            public int skuNum { get; set; }
            public decimal originalPrice { get; set; }
            public decimal price { get; set; }

            //public BaseDiscountInfo baseDiscountInfo { get; set; }
            //public BizInfo bizInfo { get; set; }
            //public string commentStatus { get; set; }
            //public string goodsCategoryId { get; set; }
            //public string goodsCode { get; set; }
            //public string goodsTitle { get; set; }
            //public int goodsType { get; set; }
            //public int hadDeliveryItemNum { get; set; }
            //public int id { get; set; }
            //public string imageUrl { get; set; }
            //public string point { get; set; }
            //public string rightsOrderId { get; set; }
            //public string rightsStatus { get; set; }
            //public string rightsStatusName { get; set; }
            //public string shouldPaymentAmount { get; set; }
            //public double skuAmount { get; set; }
            //public string skuCode { get; set; }
            //public int skuId { get; set; }
            //public string skuName { get; set; }
            //public TagInfo tagInfo { get; set; }
            //public string totalPoint { get; set; }
        }

        //public class CycleOrderInfo
        //{
        //    public string currentCycleNum { get; set; }
        //    public string cycleDeliveryTime { get; set; }
        //    public string cycleItemList { get; set; }
        //    public string cyclePackageList { get; set; }
        //    public string cycleSize { get; set; }
        //    public string cycleType { get; set; }
        //    public string cycleTypeName { get; set; }
        //    public string firstTime { get; set; }
        //}

        //public class GoodsPromotionInfo
        //{
        //    public CycleOrderInfo cycleOrderInfo { get; set; }
        //    public string goodsDistributionType { get; set; }
        //    public int promotionType { get; set; }
        //}

        //public class BaseDiscountInfo
        //{
        //    public string balanceDiscountAmount { get; set; }
        //    public string balanceDiscountDeliveryAmount { get; set; }
        //    public string couponCodeDiscountAmount { get; set; }
        //    public string couponCodeDiscountInfo { get; set; }
        //    public string couponDiscountAmount { get; set; }
        //    public string couponDiscountInfo { get; set; }
        //    public string deliveryUsedPoints { get; set; }
        //    public string memberPointsDiscountAmount { get; set; }
        //    public string membershipDiscountAmount { get; set; }
        //    public string membershipDiscountInfo { get; set; }
        //    public string merchantDiscountAmount { get; set; }
        //    public string nynjDiscountAmount { get; set; }
        //    public string pointsDiscountDeliveryAmount { get; set; }
        //    public string promotionDiscountAmount { get; set; }
        //    public string usedMemberPoints { get; set; }
        //}

        //public class BizInfo
        //{
        //    public string bizId { get; set; }
        //    public string bizOrderId { get; set; }
        //    public string bizType { get; set; }
        //    public string subBizType { get; set; }
        //}

        //public class TagInfo
        //{
        //    public string bizTagList { get; set; }
        //    public string couponTag { get; set; }
        //    public List<string> goodsBizTag { get; set; }
        //    public string goodsUserTag { get; set; }
        //    public string merchantTag { get; set; }
        //    public string userTag { get; set; }
        //}

    }

}