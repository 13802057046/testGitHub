
namespace KssService.Responses
{
    public class BaseResponse
    {
        public Code code { get; set; }
        //public object data { get; set; }
    }
    public class Code
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}