using BaseInfo;
using KssService.Requests;
using KssService.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Fw.Api.Request;
using Fw.Api.Response;
using Newtonsoft.Json;

namespace KssService.Logic
{
    public class RightsOrderLogic : BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 微盟-维权订单列表(ec/rights/searchRightsOrderList)
        /// </summary>
        public void SearchRightsOrderListFromWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 维权订单列表 ";

            SearchRightsOrderListRequest request = new SearchRightsOrderListRequest();
            request.pageNum = 1;
            request.pageSize = 100;

            SearchRightsOrderListResponse response = HttpPostToWm<SearchRightsOrderListResponse>("ec/order/queryOrderList", request, preMsg);
        }


        /// <summary>
        /// 微盟-查询订单详情(ec/order/queryOrderDetail)
        /// </summary>
        public void QueryOrderDetailFromWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 查询订单列表 ";

            QueryOrderDetailRequest request = new QueryOrderDetailRequest();
            request.orderNo = 1;
            QueryOrderDetailResponse response = HttpPostToWm<QueryOrderDetailResponse>("ec/order/queryOrderDetail", request, preMsg);
        }


        /// <summary>
        /// 退货/退款商家确认收货接口(ec/rights/confirmReceivedRightsGoods)
        /// </summary>
        public void ConfirmReceivedRightsGoodsToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 订单发货(单个) ";

            string sql = @"select a.F_BillID, F_ExpressNum, F_ExpressCode 
                        from T_OrderLogisticsInformation a ";
            DataTable dtOrder = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtOrder.Rows.Count + "行。", 1);
            if (dtOrder.Rows.Count == 0) return;


            foreach (DataRow drDetail in dtOrder.Rows)
            {
                ConfirmReceivedRightsGoodsRequest request = new ConfirmReceivedRightsGoodsRequest();
                request.id = NumUtil.ToLong(drDetail["F_BillID"]);
                ConfirmReceivedRightsGoodsResponse response = HttpPostToWm<ConfirmReceivedRightsGoodsResponse>("ec/rights/confirmReceivedRightsGoods", request, preMsg);
            }

        }

    }
}