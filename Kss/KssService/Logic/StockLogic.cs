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
    public class StockLogic : BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 微盟-商品信息-全量更新商品库存(ec/goods/wholeUpdateStock)
        /// </summary>
        public void WholeUpdateStockToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 全量更新商品库存 ";

            string sql = @"select a.F_GoodsID, a.F_SkuID, a.F_ID, a.F_Barcode, b.F_Qty, b.F_ShopID, b.F_StorageID
                            from v_Item_NoPic a 
                            left join t_StorageQty b on a.F_ID = b.F_ItemID and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID 
                            where a.F_UpdateTime >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_UpdateTime < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            DataTable dtItem = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtItem.Rows.Count + "行。", 1);
            if (dtItem.Rows.Count == 0) return;

            foreach (DataRow drItem in dtItem.Rows)
            {
                WholeUpdateStockRequest request = new WholeUpdateStockRequest();

                request.goodsId = NumUtil.ToLong(drItem["F_GoodsID"]);
                List<WholeUpdateStockSku> skuList = new List<WholeUpdateStockSku>();

                skuList.Add(new WholeUpdateStockSku
                {
                    skuId = NumUtil.ToLong(drItem["F_SkuID"]),
                    editStockNum = NumUtil.ToInt(drItem["F_Qty"])
                });
                request.storeId = NumUtil.ToLong(drItem["F_ShopID"]);
                request.warehouseId = NumUtil.ToLong(drItem["F_StorageID"]);
                request.warehouseCode = StrUtil.ConvToStr(drItem["F_StorageID"]);

                request.skuList = skuList;
                AddGoodsResponse response = HttpPostToWm<AddGoodsResponse>("ec/goods/wholeUpdateStock", request, preMsg);

            }
        }

    }
}