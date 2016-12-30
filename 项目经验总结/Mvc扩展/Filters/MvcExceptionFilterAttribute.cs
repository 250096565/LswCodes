using System.Web.Mvc;
using Exceptionless;
using Tool;
using Enums;
namespace MvcCustommade.Filters
{
    /// <summary>
    /// Mvc异常过滤器
    /// </summary>
    public class MvcExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            try
            {
                filterContext.Exception.ToExceptionless().Submit();
            }
            finally
            {
                //设置为true阻止golbal里面的错误执行
                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonResult() { Data = ResponseManager.GetOutputResponse(OutputStatus.Error), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
    }
}
