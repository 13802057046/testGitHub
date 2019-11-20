
namespace KssApi.Common.Validation
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// 无错误
        /// </summary>
        public const int NoError = 0;

        /// <summary>
        /// 签名不合法
        /// </summary>
        public const int InvalidSign = 1;

        /// <summary>
        /// 您的访问过于频繁，请稍后再试
        /// </summary>
        public const int ExceedMaxRequests = 2;

        /// <summary>
        /// 内部服务错误
        /// </summary>
        public const int InternalServerError = 3;

        /// <summary>
        /// 该订单Id不存在
        /// </summary>
        public const int DeliveryNotExist = 4;

        

    }
}