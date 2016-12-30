using Enums;
using System.Web.Http.Filters;
using Exceptionless;
using Tool;

namespace RestfulApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理Filter
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //把错误信息发送到exceptionless  地址  192.168.0.2:9400
            try
            {
                actionExecutedContext.Exception.ToExceptionless().Submit();
            }
            finally
            {
                actionExecutedContext.Response =
                ResponseHelper.CreatHttpResponseMessage(ResponseManager.GetOutputResponse(OutputStatus.Error));
            }



        }
    }
}