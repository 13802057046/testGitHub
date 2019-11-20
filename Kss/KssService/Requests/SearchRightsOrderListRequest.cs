using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-维权订单列表(ec/rights/searchRightsOrderList) 
    /// </summary>
    public class SearchRightsOrderListRequest
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public int ecBizStoreId { get; set; }
        public int rightsStatus { get; set; }
        public int createTime { get; set; }
        public int endTime { get; set; }
    }
}
