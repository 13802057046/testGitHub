using KssApi.Models.Responses;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiThrottle;

namespace KssApi.Common.Validation
{
    /// <summary>
    /// 重载基类的QuotaExceededResponse，返回自定义code和message
    /// </summary>
    public class MyThrottlingHandler : ThrottlingHandler
    {
        /// <summary>
        /// 重载基类的QuotaExceededResponse，返回自定义code和message
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="responseCode"></param>
        /// <param name="retryAfter"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> QuotaExceededResponse(HttpRequestMessage request, object content,
            HttpStatusCode responseCode, string retryAfter)
        {
            var res = new BaseResponse
            {
                Code = ErrorCodes.ExceedMaxRequests,
                Message = "您的访问过于频繁，请稍后再试"
            };
            return base.QuotaExceededResponse(request, res, HttpStatusCode.OK, retryAfter);

        }
    }
}