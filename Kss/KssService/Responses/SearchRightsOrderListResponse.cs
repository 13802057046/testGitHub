
using System;
using System.Collections.Generic;

namespace KssService.Responses
{
    /// <summary>
    /// 微盟-维权订单列表(ec/rights/searchRightsOrderList)
    /// </summary>
    public class SearchRightsOrderListResponse : BaseResponse
    {
        //public Code code { get; set; }
        public Data data { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }

        public class Data
        {
            public List<PageListItem> pageList { get; set; }
            public int pageNum { get; set; }
            public int pageSize { get; set; }
            public int totalCount { get; set; }
        }

        public class PageListItem
        {
            public long id { get; set; }
            public int skuNum { get; set; }
            public decimal price { get; set; }
            public DateTime createTime { get; set; }
            public long orderNo { get; set; }
            public decimal refundAmount { get; set; }
            public long storeId { get; set; }
        }
    }
}