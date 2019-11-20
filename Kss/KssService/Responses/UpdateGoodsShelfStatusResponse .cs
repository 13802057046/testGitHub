
namespace KssService.Responses
{
    /// <summary>
    /// 微盟-批量上下架
    /// </summary>
    public class UpdateGoodsShelfStatusResponse : BaseResponse
    {
        //public Code code { get; set; }
        public UpdateGoodsShelfStatusData data { get; set; }
    }

    public class UpdateGoodsShelfStatusData
    {
        public string deletedGoodsIds { get; set; }
        public string result { get; set; }
        public string outerGoodsIdList { get; set; }
        public string goodsId { get; set; }
        public string skuList { get; set; }
        public string distributorId { get; set; }
        public string distributorResponse { get; set; }
    }

}