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
    public class OutInLogic : BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 发网>创建出入库单接口；采购入库、调拨出库
        /// </summary>
        public void AddOutInToFw(DateTime startDate, DateTime endDate)
        {
            string preMsg = "发网 创建出入库单 ";

            string sql = @"select a.F_ShopID, a.F_Billid, a.F_Remark
                            from t_StockOrder a 
                            where a.F_CreateTime >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_CreateTime < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            DataTable dtStockOrder = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtStockOrder.Rows.Count + "行。", 1);
            if (dtStockOrder.Rows.Count == 0) return;

            foreach (DataRow drStockOrder in dtStockOrder.Rows)
            {
                FwPurchaseOutinorderAddRequest request = new FwPurchaseOutinorderAddRequest();
                WmsPurchaseOutinorderAddResponse response = new WmsPurchaseOutinorderAddResponse();
                request.ActionType = "IN";
                request.WareHouseCode = drStockOrder["F_ShopID"].ToString();
                request.SyncId = drStockOrder["F_Billid"].ToString();
                request.Remark = drStockOrder["F_Remark"].ToString();
                List<FwPurchaseOutinorderAddRequest.Item> products = new List<FwPurchaseOutinorderAddRequest.Item>();

                DataTable dtDetail = sd.GetDataTable("select b.F_BarCode, a.F_Order, a.F_Qty, a.F_Price  from t_StockOrderDetail a " +
                                                     "  left join v_Item_NoPic b on a.F_ItemID = b.F_ID and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID" +
                                                     " where F_Billid = '" + drStockOrder["F_Billid"] + "'");
                foreach (DataRow drDetail in dtDetail.Rows)
                {
                    products.Add(new FwPurchaseOutinorderAddRequest.Item()
                    {
                        BarCode = drDetail["F_BarCode"].ToString(),
                        InventoryType = "NORMAL",
                        Quantity = (int)NumUtil.ToDouble(drDetail["F_Qty"]),
                        LineNo = StrUtil.ConvToStr(drDetail["F_Order"]),
                        UnitPrice = NumUtil.ToDouble(drDetail["F_Price"]),
                        TotalPrice = NumUtil.ToDouble(drDetail["F_Price"]) * NumUtil.ToDouble(drDetail["F_Qty"])
                    });
                }
                
                request.Items = products;
                response = HttpPostToFw<WmsPurchaseOutinorderAddResponse>(request, preMsg);
            }

            Log.WriteLog(preMsg + "完成。" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + " ～ " + endDate.ToString("yyyy-MM-dd HH:mm:ss"), 1);
        }

        /// <summary>
        /// 发网>出入库单取消接口
        /// </summary>
        public void CancelOutInToFw(DateTime startDate, DateTime endDate)
        {
            string preMsg = "发网 出入库单取消接口 ";

            string sql = @"select a.F_ShopID, a.F_Billid, a.F_Remark
                            from t_DelStockIn a 
                            where a.F_DeleteDate >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_DeleteDate < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            DataTable dtStockIn = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtStockIn.Rows.Count + "行。", 1);
            if (dtStockIn.Rows.Count == 0) return;

            foreach (DataRow drStockIn in dtStockIn.Rows)
            {
                WmsPurchaseOutinorderCancelRequest request = new WmsPurchaseOutinorderCancelRequest();
                WmsPurchaseOutinorderCancelResponse response = new WmsPurchaseOutinorderCancelResponse();
                request.SyncId = drStockIn["F_Billid"].ToString();
                request.ActionType = "IN";
                request.Remark = drStockIn["F_Remark"].ToString();

                response = HttpPostToFw<WmsPurchaseOutinorderCancelResponse>(request, preMsg);
            }

            Log.WriteLog(preMsg + "完成。" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + " ～ " + endDate.ToString("yyyy-MM-dd HH:mm:ss"), 1);
        }

    }
}