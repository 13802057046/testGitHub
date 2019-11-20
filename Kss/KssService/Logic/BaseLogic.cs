using System;
using BaseInfo;
using System.IO;
using System.Net;
using System.Text;
using Fw.Api;
using Fw.Api.Request;
using KssService.Responses;
using Newtonsoft.Json;

namespace KssService.Logic
{
    public class BaseLogic
    {
        SqlDataHelp sd = new SqlDataHelp();

        public static string fw_uri = G_INI.ReadValue("Connect", "fw_uri");
        public static string fw_AppKey = G_INI.ReadValue("Connect", "fw_AppKey");
        public static string fw_AppSecret = G_INI.ReadValue("Connect", "fw_AppSecret");
        public static string fw_PartnerId = G_INI.ReadValue("Connect", "fw_PartnerId");

        public static string wm_client_id = G_INI.ReadValue("Connect", "wm_client_id");
        public static string wm_client_secret = G_INI.ReadValue("Connect", "wm_client_secret");
        public static string wm_redirect_uri = G_INI.ReadValue("Connect", "wm_redirect_uri");
        public static string wm_domain = G_INI.ReadValue("Connect", "wm_domain");
        public static string wm_code = G_INI.ReadValue("Connect", "wm_code");
        
        public T HttpPostToFw<T>(IFwRequest<T> request, string preMsg) where T : FwResponse
        {
            DefaultFwClient fwClient = new DefaultFwClient(fw_uri, fw_AppKey, fw_AppSecret, fw_PartnerId);
            T response = fwClient.Execute<T>(request);
            string param = preMsg + "接口请求URL:" + response.ReqUrl + "\n请求报文:" + response.ReqBody + "\n响应报文:" + response.Body;
            if (!response.IsSuccess)
            {
                Log.WriteLog(preMsg + " 调用API失败。" + param, 0);
                //throw new Exception(preMsg + " 调用API失败，程序处理中断。");
            }
            else
            {
                Log.WriteLog(preMsg + " 调用API成功。" + param, 1);
            }

            return response;
        }

        public T HttpPostToWm<T>(string method, object objBody, string preMsg)
        {
            string result;
            string accessToken = GetWmAccessToken();
            string domain = G_INI.ReadValue("Connect", "wm_domain");

            string uri = string.Format("https://{0}/api/1_0/{1}?accesstoken={2}", domain, method, accessToken);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            string body = JsonConvert.SerializeObject(objBody);
            byte[] buffer = Encoding.UTF8.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            response.Close();
            request.Abort();

            BaseResponse ret = JsonConvert.DeserializeObject<BaseResponse>(result);
            string param = preMsg + "接口请求URL:" + uri + "\n请求报文:" + body + "\n响应报文:" + result;
            if (ret.code.errcode != "0")
            {
                Log.WriteLog(preMsg + " 调用API失败。" + param, 0);
                //throw new Exception(preMsg + " 调用API失败，程序处理中断。");
            }
            else
            {
                Log.WriteLog(preMsg + " 调用API成功。" + param, 1);
            }

            return JsonConvert.DeserializeObject<T>(result);

        }

        public string GetWmAccessToken()
        {
            DateTime lastGetDateTime = DateTime.Parse(G_INI.ReadValue("Connect", "wm_access_token_update_time"));
            if (DateTime.Now.AddMinutes(-60) < lastGetDateTime)
            {
                //AccessToken没过期还能用
                string wm_access_token = G_INI.ReadValue("Connect", "wm_access_token");
                return wm_access_token;
            }

            string uri = "";
            if (lastGetDateTime < DateTime.Now.AddHours(-7*24))
            {
                //通过code，获取AccessToken
                string wm_access_token_uri = G_INI.ReadValue("Connect", "wm_access_token_uri");
                uri = String.Format(wm_access_token_uri, wm_domain, wm_code, wm_client_id, wm_client_secret, wm_redirect_uri);
            }
            else
            {
                //通过refreshToken，刷新AccessToken
                string wm_refresh_token_uri = G_INI.ReadValue("Connect", "wm_refresh_token_uri");
                string wm_refresh_token = G_INI.ReadValue("Connect", "wm_refresh_token");
                uri = String.Format(wm_refresh_token_uri, wm_domain, wm_client_id, wm_client_secret, wm_refresh_token);
            }

            string result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            response.Close();
            request.Abort();

            WmTokenResponse wmAccessToken = JsonConvert.DeserializeObject<WmTokenResponse>(result);

            G_INI.Writue("Connect", "wm_access_token", wmAccessToken.access_token);
            G_INI.Writue("Connect", "wm_refresh_token", wmAccessToken.refresh_token);
            G_INI.Writue("Connect", "wm_access_token_update_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return wmAccessToken.access_token;

        }


        
        ///// <summary>
        ///// 写入SHP的Log
        ///// </summary>
        //public void WriteWMSlog(string F_FileName, string F_BillType, string F_SHPBillID, string F_WMSBillID, string F_Message)
        //{
        //    Log.WriteLog(F_Message, 1);
        //    string sqlf = String.Format("INSERT INTO [dbo].[t_WMSlog] ([F_FileName] ,[F_BillType] ,[F_Sender] ,[F_Date] ,[F_SHPBillID] ,[F_WMSBillID] ,[F_Message] ,[F_FLG] ,[F_Check]) VALUES ('{0}' ,'{1}' ,'GYY' ,getdate() ,'{2}' ,'{3}' ,'{4}' ,0 ,0)",
        //        F_FileName, F_BillType, F_SHPBillID, F_WMSBillID, F_Message);
        //    int res = sd.ExcuteNonQuery(sqlf);
        //}

        ///// <summary>
        ///// 检查API返回结果，如果错误，设置全局GlobalSuccessFlag=false。最後，不更新INI文件的上次完成时间
        ///// </summary>
        //public bool CheckResult(BaseResponse rsp)
        //{
        //    if (!rsp.success)
        //    {
        //        if (rsp.errorDesc == "订单已发货，修改失败" || rsp.errorDesc == "此发货单已作废，不允许修改")
        //        {
        //        }
        //        else
        //        {
        //            SyncFineexData.GlobalSuccessFlag = false;
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}