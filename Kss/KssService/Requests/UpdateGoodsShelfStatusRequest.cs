using System.Collections.Generic;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-批量上下架
    /// </summary>
    public class UpdateGoodsShelfStatusRequest //: BaseRequest
    {
        public List<long> goodsIdList { get; set; }
        public int isPutAway { get; set; }     
        
    }


}