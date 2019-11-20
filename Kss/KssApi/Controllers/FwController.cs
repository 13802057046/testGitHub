using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using KssApi.Logics;
using KssApi.Models.Responses;
using NLog;
using System.Web.Http;
using BaseInfo;
using KssApi.Common;

namespace KssApi.Controllers
{
    /// <summary>
    /// 发网接口
    /// </summary>
    [RoutePrefix("api/fw")]
    public class FwController : ApiController
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private LogisticsLogic logisticsLogic = new LogisticsLogic();

        /// <summary>
        /// 发网出入库单确认接口
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="app_key">app_key</param>
        /// <param name="partner_id">partner_id</param>
        /// <param name="v">版本</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        [HttpPost, Route("service")]
        public HttpResponseMessage Service(string method, string app_key, string partner_id, string v, string timestamp, string sign)
        {
            string strXml = "";
            var stream = HttpContext.Current.Request.InputStream;
            stream.Position = 0;
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                strXml = streamReader.ReadToEndAsync().Result;
                stream.Position = 0;
            }
            var app_secret = System.Configuration.ConfigurationManager.AppSettings["app_secret"];
            Dictionary<string, string> txtParams = new Dictionary<string, string>();
            txtParams.Add("appKey", app_key);
            txtParams.Add("method", method);
            txtParams.Add("partner_id", partner_id);
            txtParams.Add("timestamp", timestamp);
            txtParams.Add("v", v);

            string validSign = SignUtil.SignTopRequest(txtParams, app_secret, strXml);
            logger.Info("请求参数签名SIGN" + ":" + validSign);

            //签名不合法
            if (validSign != sign)
            {
                var errmsg = new FwResponse
                {
                    IsSuccess = false,
                    ErrCode = "S00005",
                    ErrMsg = "sign验证失败"
                };
                return MakeReturnMessage(errmsg);
            }

            switch (method)
            {
                case "fineex.wms.purchase.outinorder.confirm": //fineex.wms.purchase.outinorder.confirm (出入库单确认接口）
                    FwPurchaseOutinorderConfirmRequest reqComfirm =
                        XmlSerializeHelper.DESerializer<FwPurchaseOutinorderConfirmRequest>(strXml);

                    StringBuilder sbTemp = new StringBuilder();
                    sbTemp.AppendLine("INSERT INTO t_StockIn						");
                    sbTemp.AppendLine("           (F_BillID                         ");
                    sbTemp.AppendLine("           ,F_Date                           ");
                    sbTemp.AppendLine("           ,F_SupplierID                     ");
                    sbTemp.AppendLine("           ,F_BillType                       ");
                    sbTemp.AppendLine("           ,F_Money                          ");
                    sbTemp.AppendLine("           ,F_PayMoney                       ");
                    sbTemp.AppendLine("           ,F_ShopID                         ");
                    sbTemp.AppendLine("           ,F_StorageID                      ");
                    sbTemp.AppendLine("           ,F_Remark                         ");
                    sbTemp.AppendLine("           ,F_BillMan                        ");
                    sbTemp.AppendLine("           ,F_Check                          ");
                    sbTemp.AppendLine("           ,F_Checker                        ");
                    sbTemp.AppendLine("           ,F_CheckDate                      ");
                    sbTemp.AppendLine("           ,F_CutOff                         ");
                    sbTemp.AppendLine("           ,F_IsUp                           ");
                    sbTemp.AppendLine("           ,F_BuyerID                        ");
                    sbTemp.AppendLine("           ,F_BuyerName                      ");
                    sbTemp.AppendLine("           ,F_TallyID                        ");
                    sbTemp.AppendLine("           ,F_TallyName                      ");
                    sbTemp.AppendLine("           ,F_Reason                         ");
                    sbTemp.AppendLine("           ,F_Dist                           ");
                    sbTemp.AppendLine("           ,F_AutoStockInDate                ");
                    sbTemp.AppendLine("           ,F_SpaceStockInDate               ");
                    sbTemp.AppendLine("           ,F_LuckyBag                       ");
                    sbTemp.AppendLine("           ,F_Creater                        ");
                    sbTemp.AppendLine("           ,F_CreateTime                     ");
                    sbTemp.AppendLine("           ,F_Updater                        ");
                    sbTemp.AppendLine("           ,F_UpdateTime)                    ");
                    sbTemp.AppendLine("     VALUES                                  ");
                    sbTemp.AppendLine("           ('" + reqComfirm.SyncId + "'      "); //F_BillID
                    sbTemp.AppendLine("           ,getdate()                        "); //F_Date
                    sbTemp.AppendLine("           ,F_SupplierID                     "); //F_SupplierID      
                    sbTemp.AppendLine("           ,F_BillType                       "); //F_BillType        
                    sbTemp.AppendLine("           ,F_Money                          "); //F_Money           
                    sbTemp.AppendLine("           ,F_PayMoney                       "); //F_PayMoney        
                    sbTemp.AppendLine("           ('" + reqComfirm.WareHouseCode + "'"); //F_ShopID          
                    sbTemp.AppendLine("           ,F_StorageID                      "); //F_StorageID       
                    sbTemp.AppendLine("           ('" + reqComfirm.Remark + "'      "); //F_Remark          
                    sbTemp.AppendLine("           ,F_BillMan                        "); //F_BillMan         
                    sbTemp.AppendLine("           ,F_Check                          "); //F_Check           
                    sbTemp.AppendLine("           ,F_Checker                        "); //F_Checker         
                    sbTemp.AppendLine("           ('" + reqComfirm.OrderConfirmTime + "'"); //F_CheckDate       
                    sbTemp.AppendLine("           ,F_CutOff                         "); //F_CutOff          
                    sbTemp.AppendLine("           ,F_IsUp                           "); //F_IsUp            
                    sbTemp.AppendLine("           ,F_BuyerID                        "); //F_BuyerID         
                    sbTemp.AppendLine("           ,F_BuyerName                      "); //F_BuyerName       
                    sbTemp.AppendLine("           ,F_TallyID                        "); //F_TallyID         
                    sbTemp.AppendLine("           ,F_TallyName                      "); //F_TallyName       
                    sbTemp.AppendLine("           ,F_Reason                         "); //F_Reason          
                    sbTemp.AppendLine("           ,F_Dist                           "); //F_Dist            
                    sbTemp.AppendLine("           ,F_AutoStockInDate                "); //F_AutoStockInDate 
                    sbTemp.AppendLine("           ,F_SpaceStockInDate               "); //F_SpaceStockInDate
                    sbTemp.AppendLine("           ,F_LuckyBag                       "); //F_LuckyBag        
                    sbTemp.AppendLine("           ,F_Creater                        "); //F_Creater         
                    sbTemp.AppendLine("           ,F_CreateTime                     "); //F_CreateTime      
                    sbTemp.AppendLine("           ,F_Updater                        "); //F_Updater         
                    sbTemp.AppendLine("           ,F_UpdateTime)                    "); //F_UpdateTime  

                    foreach (var drItem in reqComfirm.Items)
                    {
                        sbTemp.AppendLine("INSERT INTO t_StockInDetail			"); //
                        sbTemp.AppendLine("           (F_BillID                 "); //
                        sbTemp.AppendLine("           ,F_Order                  "); //
                        sbTemp.AppendLine("           ,F_ItemID                 "); //
                        sbTemp.AppendLine("           ,F_ColorID                "); //
                        sbTemp.AppendLine("           ,F_SizeID                 "); //
                        sbTemp.AppendLine("           ,F_Qty                    "); //
                        sbTemp.AppendLine("           ,F_Price                  "); //
                        sbTemp.AppendLine("           ,F_Discount               "); //
                        sbTemp.AppendLine("           ,F_Rate                   "); //
                        sbTemp.AppendLine("           ,F_Remark                 "); //
                        sbTemp.AppendLine("           ,F_LinkBill               "); //
                        sbTemp.AppendLine("           ,F_CostPrice              "); //
                        sbTemp.AppendLine("           ,F_QualFlg                "); //
                        sbTemp.AppendLine("           ,F_PosPrice)              "); //
                        sbTemp.AppendLine("     VALUES                          "); //
                        sbTemp.AppendLine("           (F_BillID                 "); //F_BillID   
                        sbTemp.AppendLine("           ,F_Order                  "); //F_Order    
                        sbTemp.AppendLine("           ,F_ItemID                 "); //F_ItemID   
                        sbTemp.AppendLine("           ,F_ColorID                "); //F_ColorID  
                        sbTemp.AppendLine("           ,F_SizeID                 "); //F_SizeID   
                        sbTemp.AppendLine("           ,F_Qty                    "); //F_Qty      
                        sbTemp.AppendLine("           ,F_Price                  "); //F_Price    
                        sbTemp.AppendLine("           ,F_Discount               "); //F_Discount 
                        sbTemp.AppendLine("           ,F_Rate                   "); //F_Rate     
                        sbTemp.AppendLine("           ,F_Remark                 "); //F_Remark   
                        sbTemp.AppendLine("           ,F_LinkBill               "); //F_LinkBill 
                        sbTemp.AppendLine("           ,F_CostPrice              "); //F_CostPrice
                        sbTemp.AppendLine("           ,F_QualFlg                "); //F_QualFlg  
                        sbTemp.AppendLine("           ,F_PosPrice)              "); //F_PosPrice
                    }

                    //返回结果成功
                    return MakeReturnMessage(new FwPurchaseOutinorderConfirmResponse
                    {
                        IsSuccess = true,
                        ErrCode = "0",
                        ErrMsg = ""
                    });
                    break;
                case "fineex.wms.trade.orderprocess.report": //fineex.wms.trade.orderdetail.confirm (订单确认接口（带明细））
                    FwPurchaseOutinorderConfirmRequest req2 =
                        XmlSerializeHelper.DESerializer<FwPurchaseOutinorderConfirmRequest>(strXml);
                    //返回结果成功
                    return MakeReturnMessage(new FwPurchaseOutinorderConfirmResponse
                    {
                        IsSuccess = true,
                        ErrCode = "0",
                        ErrMsg = ""
                    });
                    break;
                case "fineex.wms.trade.returnorder.confirm": //fineex.wms.trade.returnorder.confirm (退货单确认接口)
                    FwPurchaseOutinorderConfirmRequest req3 =
                        XmlSerializeHelper.DESerializer<FwPurchaseOutinorderConfirmRequest>(strXml);
                    //返回结果成功
                    return MakeReturnMessage(new FwPurchaseOutinorderConfirmResponse
                    {
                        IsSuccess = true,
                        ErrCode = "0",
                        ErrMsg = ""
                    });
                    break;
            }

            return MakeReturnMessage(new FwResponse
            {
                IsSuccess = false,
                ErrCode = "S00003",
                ErrMsg = "不存在的方法名"
            });

        }

        private static HttpResponseMessage MakeReturnMessage<T>(T returnMsg)
        {
            string res = XmlSerializeHelper.Serialize<T>(returnMsg);
            return new HttpResponseMessage { Content = new StringContent(res, Encoding.GetEncoding("UTF-8"), "application/x-www-form-urlencoded") };
        }

    }
}