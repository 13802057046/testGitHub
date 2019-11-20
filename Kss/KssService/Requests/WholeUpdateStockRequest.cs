using System.Collections.Generic;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-全量更新商品库存
    /// </summary>
    public class WholeUpdateStockRequest //: BaseRequest
    {
        public long goodsId { get; set; }
        public List<WholeUpdateStockSku> skuList { get; set; }
        public long storeId { get; set; }
        public long warehouseId { get; set; }
        public string warehouseCode { get; set; }
        
    }

    public class WholeUpdateStockSku
    {
        public long skuId { get; set; }
        public int editStockNum { get; set; }
    }

}