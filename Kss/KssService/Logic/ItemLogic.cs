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
    public class ItemLogic : BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        /// <summary>
        /// 发网>商品信息
        /// </summary>
        public void AddProductToFw(DateTime startDate, DateTime endDate)
        {
            string preMsg = "发网 商品信息 ";

            string sql = @"select a.F_BarCode, a.F_ID, a.F_Name, a.F_Unit, a.F_Safe, a.F_Spec
                            from v_Item_NoPic a 
                            where a.F_CreaterDate >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_CreaterDate < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            DataTable dtItem = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtItem.Rows.Count + "行。", 1);
            if (dtItem.Rows.Count == 0) return;

            WmsProductsSyncRequest request = new WmsProductsSyncRequest();
            WmsProductsSyncResponse response = new WmsProductsSyncResponse();
            List<WmsProductsSyncRequest.Item> products = new List<WmsProductsSyncRequest.Item>();

            for (int i = 0; i < dtItem.Rows.Count; i++)
            {
                products.Add(new WmsProductsSyncRequest.Item()
                {
                    BarCode = StrUtil.ConvToStr(dtItem.Rows[i]["F_BarCode"]),
                    ItemCode = StrUtil.ConvToStr(dtItem.Rows[i]["F_ID"]),
                    ActionType = "ADD",
                    ItemName = StrUtil.ConvToStr(dtItem.Rows[i]["F_Name"]),
                    Unit = StrUtil.ConvToStr(dtItem.Rows[i]["F_Unit"]),
                    AlterStorageAmount = NumUtil.ToInt(dtItem.Rows[i]["F_Safe"]),
                    PackageSpec = StrUtil.ConvToStr(dtItem.Rows[i]["F_Spec"])
                });

                if (products.Count == 100)
                {
                    request.Items = products;
                    response = HttpPostToFw<WmsProductsSyncResponse>(request, preMsg);
                    products.Clear();
                }
            }
            if (products.Count > 0)
            {
                request.Items = products;
                response = HttpPostToFw<WmsProductsSyncResponse>(request, preMsg);
            }
            Log.WriteLog(preMsg + "完成。" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + " ～ " + endDate.ToString("yyyy-MM-dd HH:mm:ss"), 1);
        }

        /// <summary>
        /// 微盟-商品信息-添加商品(ec/goods/addGoods)
        /// </summary>
        public void AddGoodsToWm(DateTime startDate, DateTime endDate)
        {
            
            string preMsg = "微盟 添加商品 ";

            string sql = @"select a.F_GoodsID, a.F_SkuID, a.F_CategoryID, a.F_Name, a.F_ID, a.F_Stop, z.F_PosPrice, a.F_CostPrice, b.F_Qty
                            from v_Item_NoPic a 
                            left join (select F_ItemZID,F_PosPrice from t_ItemZonePriceOne where F_ZoneID = '01') z on a.F_ID = z.F_ItemZID 
                            left join t_StorageQty b on a.F_ID = b.F_ItemID and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID 
                            where a.F_CreaterDate >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_CreaterDate < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_GoodsID is null ";

            DataTable dtItem = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtItem.Rows.Count + "行。", 1);
            if (dtItem.Rows.Count == 0) return;

            foreach (DataRow drItem in dtItem.Rows)
            {
                AddGoodsRequest request = new AddGoodsRequest();
                PendingAddGoodsVo goods = new PendingAddGoodsVo();
                goods.title = drItem["F_Name"].ToString();
                goods.isMultiSku = 0;
                goods.goodsImageUrl = new List<string> { G_INI.ReadValue("Local", "goodsImageUrl") };
                goods.initialSales = 0;
                goods.deductStockType = 1;
                goods.isPutAway = NumUtil.ToInt(drItem["F_Stop"]);
                goods.Sort = 0;
                goods.categoryId = NumUtil.ToLong(drItem["F_CategoryID"]);
                goods.b2cGoods = new PendingB2CGoodsVo
                {
                    freightTemplateId = NumUtil.ToLong(G_INI.ReadValue("Local", "freightTemplateId")),
                    deliveryTypeIdList = new List<long> { NumUtil.ToLong(G_INI.ReadValue("Local", "deliveryTypeId")) },
                    b2cGoodsType = 0
                };
                goods.skuList = new List<PendingSkuVo>
                {
                    new PendingSkuVo{
                        outerSkuCode = drItem["F_ID"].ToString(),
                        salePrice = NumUtil.ToDecimal(drItem["F_PosPrice"]),
                        costPrice = NumUtil.ToDecimal(drItem["F_CostPrice"]),
                        editStockNum = NumUtil.ToInt(drItem["F_Qty"]),
                        b2cSku = new PendingB2CSkuVo
                        {
                            weight=0,
                            volume=0
                        }
                    }
                };
                request.goods = goods;
                AddGoodsResponse response = HttpPostToWm<AddGoodsResponse>("ec/goods/addGoods", request, preMsg);
                sql = "update t_Item set F_GoodsID = " + response.data.goodsId +
                             " where F_ID = '" + StrUtil.ConvToStr(drItem["F_ID"]) + "';";
                if (response.data.skuList.Count > 0)
                {
                    sql += "update t_ItemDetail set F_SkuID = " + response.data.skuList[0].skuId +
                           " where F_ItemID = '" + StrUtil.ConvToStr(drItem["F_ID"]) + "';";
                }

                int cnt = sd.ExcuteNonQuery(sql);
            }

        }

        /// <summary>
        /// 微盟-商品信息-更新商品(ec/goods/updateGoods)
        /// </summary>
        public void UpdateGoodsToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 更新商品 ";

            string sql = @"select a.F_GoodsID, a.F_SkuID, a.F_CategoryID, a.F_Name, a.F_ID, a.F_Stop, z.F_PosPrice, a.F_CostPrice, b.F_Qty
                            from v_Item_NoPic a 
                            left join (select F_ItemZID,F_PosPrice from t_ItemZonePriceOne where F_ZoneID = '01') z on a.F_ID = z.F_ItemZID 
                            left join t_StorageQty b on a.F_ID = b.F_ItemID and a.F_ColorID = b.F_ColorID and a.F_SizeID = b.F_SizeID 
                            where a.F_UpdateTime >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_UpdateTime < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_GoodsID is not null ";

            DataTable dtItem = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtItem.Rows.Count + "行。", 1);
            if (dtItem.Rows.Count == 0) return;

            foreach (DataRow drItem in dtItem.Rows)
            {
                QueryGoodsDetailRequest queryReq = new QueryGoodsDetailRequest();
                queryReq.goodsId = NumUtil.ToLong(drItem["F_GoodsID"]);
                QueryGoodsDetailResponse queryRsp = HttpPostToWm<QueryGoodsDetailResponse>("ec/goods/queryGoodsDetail", queryReq, preMsg);

                UpdateGoodsRequest request = new UpdateGoodsRequest();
                PendingUpdateGoodsVo goods = new PendingUpdateGoodsVo();
                goods.goodsId = NumUtil.GetVal_Long(drItem["F_GoodsID"]);
                goods.title = drItem["F_Name"].ToString();
                goods.isMultiSku = 0;
                goods.goodsImageUrl = queryRsp.data.goods.goodsImageUrl;
                //goods.goodsDesc = "";
                goods.initialSales = 0;
                goods.deductStockType = 1;
                goods.isPutAway = NumUtil.ToInt(drItem["F_Stop"]);
                goods.Sort = 0;
                goods.categoryId = NumUtil.ToLong(drItem["F_CategoryID"]);
                goods.b2cGoods = new PendingB2CGoodsVo
                {
                    freightTemplateId = NumUtil.ToLong(G_INI.ReadValue("Local", "freightTemplateId")),
                    deliveryTypeIdList = new List<long> { NumUtil.ToLong(G_INI.ReadValue("Local", "deliveryTypeId")) },
                    b2cGoodsType = 0
                };
                goods.skuList = new List<PendingSkuVo>
                {
                    new PendingSkuVo{
                        skuId = NumUtil.ToLong(drItem["F_SkuID"]),
                        outerSkuCode = drItem["F_ID"].ToString(),
                        salePrice = NumUtil.ToDecimal(drItem["F_CostPrice"]),
                        costPrice = NumUtil.ToInt(drItem["F_Qty"]),
                        editStockNum = 0,
                        b2cSku = new PendingB2CSkuVo
                        {
                            weight=0,
                            volume=0
                        }
                    }
                };
                request.goods = goods;
                UpdateGoodsResponse response = HttpPostToWm<UpdateGoodsResponse>("ec/goods/updateGoods", request, preMsg);
            }

        }

        /// <summary>
        /// 微盟-商品信息-批量上下架(ec/goods/updateGoodsShelfStatus)
        /// </summary>
        public void UpdateGoodsShelfStatusToWm(DateTime startDate, DateTime endDate)
        {
            string preMsg = "微盟 批量上下架 ";

            string sql = @"select a.F_ID, F_GoodsID, F_Stop
                            from v_Item_NoPic a 
                            where a.F_UpdateTime >= '" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_UpdateTime < '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                              and a.F_GoodsID is not null ";

            DataTable dtItem = sd.GetDataTable(sql);
            Log.WriteLog(preMsg + "取得的记录数:" + dtItem.Rows.Count + "行。", 1);
            if (dtItem.Rows.Count == 0) return;

            foreach (DataRow drItem in dtItem.Rows)
            {
                UpdateGoodsShelfStatusRequest request = new UpdateGoodsShelfStatusRequest();

                List<long> goodsIdList = new List<long>();

                goodsIdList.Add(NumUtil.GetVal_Long(drItem["F_GoodsID"]));
                request.goodsIdList = goodsIdList;
                request.isPutAway = NumUtil.ToInt(drItem["F_Stop"]);

                UpdateGoodsShelfStatusResponse response = HttpPostToWm<UpdateGoodsShelfStatusResponse>("ec/goods/updateGoodsShelfStatus", request, preMsg);
            }
        }

        ///// <summary>
        ///// 微盟-商品信息-上传商品图片信息（文件上传）(ec/goodsImage/uploadImg)
        ///// </summary>
        //public void UploadImgToWm(DateTime startDate, DateTime endDate)
        //{

        //    //参考https://www.xin3721.com/ArticlecSharp/c11903.html

        //    // 设置参数
        //    string accessToken = GetWmAccessToken();
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://dopen.weimob.com/media/1_0/ec/goodsImage/uploadImg?accesstoken=" + accessToken);
        //    CookieContainer cookieContainer = new CookieContainer();
        //    request.CookieContainer = cookieContainer;
        //    request.AllowAutoRedirect = true;
        //    request.Method = "POST";
        //    string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
        //    request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
        //    byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
        //    byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

        //    //请求头部信息
        //    StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", "test"));
        //    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

        //    FileStream fs = new FileStream("C:\\WangBin\\文档\\test.jpg", FileMode.Open, FileAccess.Read);
        //    byte[] bArr = new byte[fs.Length];
        //    fs.Read(bArr, 0, bArr.Length);
        //    fs.Close();

        //    Stream postStream = request.GetRequestStream();
        //    postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
        //    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
        //    postStream.Write(bArr, 0, bArr.Length);
        //    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
        //    postStream.Close();

        //    //发送请求并获取相应回应数据
        //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //    //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //    Stream instream = response.GetResponseStream();
        //    StreamReader sr = new StreamReader(instream, Encoding.UTF8);
        //    //返回结果网页（html）代码
        //    string content = sr.ReadToEnd();

        //}
    }
}