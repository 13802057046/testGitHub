using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-获取商品详情(ec/retailGoods/queryGoodsDetail)
    /// </summary>
    public class QueryGoodsDetailRequest
    {
        public long goodsId { get; set; }
    }

}
