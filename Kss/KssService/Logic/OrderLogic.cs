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
    public class OrderLogic : BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 微盟-查询订单列表(ec/order/queryOrderList)
        /// </summary>
        public void QueryOrderListFromWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 查询订单列表 ";

            QueryOrderListRequest request = new QueryOrderListRequest();
            request.pageNum = 1;
            request.pageSize = 100;
            request.queryParameter = new QueryOrderListRequest.QueryParameter
            {
                orderTypes = new List<int> { 1 },
                orderStatuses = new List<int> { 1 },
                channelTypes = new List<int> { 1 },
                deliveryStatuses = new List<int> { 0 }
            };

            QueryOrderListResponse response = HttpPostToWm<QueryOrderListResponse>("ec/order/queryOrderList", request, preMsg);
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
        /// 发网>订单创建接口
        /// </summary>
        public void AddTradesToFw(DateTime startDate, DateTime endDate)
        {
            string preMsg = "发网 订单创建接口 ";

            string sql = @"select 
	                         a.F_ShopID
	                        ,a.F_BillId
	                        ,c.F_ExpressCode
	                        ,convert(varchar(100), a.F_Date, 20) as F_Date
	                        ,c.F_DeliveryPlanTime
	                        ,a.F_BillMoney
	                        ,c.F_Freight
	                        ,c.F_TotalAmount
	                        ,c.F_Payment
	                        ,c.F_InsuranceValue
	                        ,c.F_CommunitySyncCode
	                        ,c.F_BuyerNick
	                        ,a.F_Remark as F_Remark_Pos
	                        ,c.F_SourcePlatformCode
	                        ,c.F_Remark as F_Remark_OrderLogisticsInformation
	                        ,c.F_isPrintInvoice
	                        ,c.F_country
	                        ,c.F_dropoffType
	                        ,c.F_serviceType
	                        ,c.F_packagingType
	                        ,c.F_name
	                        ,c.F_zipCode
	                        ,c.F_phone
	                        ,c.F_mobilePhone
	                        ,c.F_province
	                        ,c.F_city
	                        ,c.F_county
	                        ,c.F_address
	                        ,c.F_address2
	                        ,c.F_senderName
	                        ,c.F_senderPhone
	                        ,c.F_senderMobile
                        from t_Pos a
                        left join T_OrderLogisticsInformation c on a.F_BillID = c.F_BillID
                        where a.F_Status='正常'
                          and a.F_BuildDate >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                          and a.F_BuildDate < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            DataTable dtPos = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtPos.Rows.Count + "行。", 1);
            if (dtPos.Rows.Count == 0) return;

            foreach (DataRow drPos in dtPos.Rows)
            {
                WmsTradesAddRequest request = new WmsTradesAddRequest();
                WmsTradesAddResponse response = new WmsTradesAddResponse();
                WmsTradesAddRequest.Receiver receiver = new WmsTradesAddRequest.Receiver();
                receiver.Name = drPos["F_name"].ToString();
                receiver.ZipCode = drPos["F_zipCode"].ToString();
                receiver.Phone = drPos["F_phone"].ToString();
                receiver.MobilePhone = drPos["F_mobilePhone"].ToString();
                receiver.Province = drPos["F_province"].ToString();
                receiver.City = drPos["F_city"].ToString();
                receiver.County = drPos["F_county"].ToString();
                receiver.Address = drPos["F_address"].ToString();
                //receiver.address2			= drPos["F_address2"].ToString(); todo

                //sender					List				
                //senderName				String	32	是	发货人姓名	T_OrderLogisticsInformation.F_senderName
                //    senderPhone				String	32	否	发货人移动电话	T_OrderLogisticsInformation.F_senderPhone
                //    senderMobile				String	32	否	发货人固定电话	T_OrderLogisticsInformation.F_senderMobile

                List<WmsTradesAddRequest.Item> products = new List<WmsTradesAddRequest.Item>();
                sql = @"select 
                             v.F_BarCode
                            ,v.F_Name
                            ,a.F_NewPrice
                            ,a.F_Qty 
                        from t_PosDetail a 
                        left join V_Item_Pos v on a.F_ItemID = v.F_ID and a.F_ColorID = v.F_ColorID and a.F_SizeID = v.F_SizeID 
                        where F_Billid = '" + drPos["F_Billid"] + "'";
                DataTable dtDetail = sd.GetDataTable(sql);
                foreach (DataRow drDetail in dtDetail.Rows)
                {
                    products.Add(new WmsTradesAddRequest.Item()
                    {
                        BarCode = drDetail["F_BarCode"].ToString(),
                        ItemName = drDetail["F_Name"].ToString(),
                        Price = NumUtil.ToDouble(drDetail["F_NewPrice"]),
                        Quantity = NumUtil.ToInt(drDetail["F_Qty"]),
                    });
                }
                
                //containerPack					List				
                //Pack				List				
                //containerBarcode			String		否	包材编码	t_POSDetail.F_BarCode
                //    skus			List				
                //    sku		List				
                //    barCode	String	64	否	商品条码	t_POSDetail.F_BarCode
                //    quantity	int		否	数量	t_POSDetail.F_Qty
                List<WmsTradesAddRequest.Order> orders = new List<WmsTradesAddRequest.Order>();
                orders.Add(new WmsTradesAddRequest.Order()
                {
                    WareHouseCode = drPos["F_ShopID"].ToString(),
                    SaleOrderCode = drPos["F_BillId"].ToString(),
                    //orderType = 0, todo
                    LogisticsCode = drPos["F_ExpressCode"].ToString(),
                    SaleDate = drPos["F_Date"].ToString(),
                    //deliveryPlanTime = drPos["F_DeliveryPlanTime"].ToString(), tod
                    ItemAmount = NumUtil.ToDouble(drPos["F_BillMoney"]),
                    Freight = NumUtil.ToDouble(drPos["F_Freight"]),
                    TotalAmount = NumUtil.ToDouble(drPos["F_TotalAmount"]),
                    Payment = NumUtil.ToDouble(drPos["F_Payment"]),
                    //insuranceValue = NumUtil.ToDouble(drPos[".F_InsuranceValue"]), todo
                    //communitySyncCode = drPos["F_CommunitySyncCode"].ToString(),
                    BuyerNick = drPos["F_BuyerNick"].ToString(),
                    TradeId = drPos["F_Remark_Pos"].ToString(),
                    SourcePlatformCode = drPos["F_SourcePlatformCode"].ToString(),
                    Remark = drPos["F_Remark_OrderLogisticsInformation"].ToString(),
                    //isPrintInvoice = NumUtil.ToBoolean(drPos["F_isPrintInvoice"]), todo
                    //country = drPos["F_country"].ToString(), todo
                    //dropoffType = drPos["F_dropoffType"].ToString(), todo
                    //serviceType = drPos["F_serviceType"].ToString(), todo
                    //packagingType = drPos["F_packagingType"].ToString(), todo
                    Receiver = receiver,
                    Items = products
                });
                request.Orders = orders;

                response = HttpPostToFw<WmsTradesAddResponse>(request, preMsg);
            }

            Log.WriteLog(preMsg + "完成。" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + " ～ " + endDate.ToString("yyyy-MM-dd HH:mm:ss"), 1);
        }

        /// <summary>
        /// 微盟-更新订单标记(ec/order/updateOrderFlag)
        /// </summary>
        public void UpdateOrderFlagToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 更新订单标记 ";

            string sql = @"select a.F_BillStatus, a.F_BillID 
                        from T_OrderLogisticsInformation a ";
            DataTable dtOrder = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtOrder.Rows.Count + "行。", 1);
            if (dtOrder.Rows.Count == 0) return;

           
            foreach (DataRow drDetail in dtOrder.Rows)
            {
                UpdateOrderFlagRequest request = new UpdateOrderFlagRequest();
                request.flagRank = NumUtil.ToInt(drDetail["F_BillStatus"]);
                request.orderNoList = new List<string> { drDetail["F_BillID"].ToString() };
                UpdateOrderFlagResponse response = HttpPostToWm<UpdateOrderFlagResponse>("ec/order/updateOrderFlag", request, preMsg);
            }

        }


        /// <summary>
        /// 微盟-订单发货(单个)(ec/order/deliveryOrder)
        /// </summary>
        public void DeliveryOrderToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 订单发货(单个) ";

            string sql = @"select a.F_BillID, F_ExpressNum, F_ExpressCode 
                        from T_OrderLogisticsInformation a ";
            DataTable dtOrder = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtOrder.Rows.Count + "行。", 1);
            if (dtOrder.Rows.Count == 0) return;


            foreach (DataRow drDetail in dtOrder.Rows)
            {
                DeliveryOrderRequest request = new DeliveryOrderRequest();
                request.orderNo = NumUtil.ToLong(drDetail["F_BillID"]);
                request.deliveryNo = drDetail["F_ExpressNum"].ToString();
                request.deliveryCompanyCode = drDetail["F_ExpressCode"].ToString();
                DeliveryOrderResponse response = HttpPostToWm<DeliveryOrderResponse>("ec/order/deliveryOrder", request, preMsg);
            }

        }
    }
}