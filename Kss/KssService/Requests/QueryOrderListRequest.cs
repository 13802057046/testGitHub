using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KssService.Requests
{
    /// <summary>
    /// 微盟-查询订单列表(ec/order/queryOrderList)
    /// </summary>
    public class QueryOrderListRequest
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public QueryParameter queryParameter { get; set; }
        public class QueryParameter
        {
            public List<int> orderTypes { get; set; }
            public List<int> orderStatuses { get; set; }
            public List<int> channelTypes { get; set; }
            public List<int> deliveryStatuses { get; set; }
            
            //public int createStartTime { get; set; }
            //public int createEndTime { get; set; }
            //public int updateStartTime { get; set; }
            //public int updateEndTime { get; set; }
            //public string keyword { get; set; }
            //public string searchType { get; set; }
            //public List<string> paymentMethods { get; set; }
            //public List<int> paymentTypes { get; set; }
            //public List<int> flagRanks { get; set; }
            //public List<int> bizTypes { get; set; }
            //public List<int> deliveryTypes { get; set; }
        }
    }

}
