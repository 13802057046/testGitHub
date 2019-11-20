using KssApi.Models.Responses;
using NLog;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace KssApi.Common.Validation
{
    /// <summary>
    /// 全局错误的终结捕获者
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //对外的显示
            string errorContent = "服务器内部错误：" + actionExecutedContext.Exception.Message;

            //对内的日志
            logger.Error(errorContent + "\r\n" + actionExecutedContext.Exception.StackTrace);

            //对外的返回S
            var res = new BaseResponse
            {
                Code = ErrorCodes.InternalServerError,
                Message = errorContent
            };
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, res);

            base.OnException(actionExecutedContext);
        }
    }
}