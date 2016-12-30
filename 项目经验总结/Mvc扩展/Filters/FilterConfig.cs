using System.Web.Mvc;

namespace MvcCustommade.Filters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //全局异常过滤
            filters.Add(new MvcExceptionFilterAttribute());

            filters.Add(new MvcActionProcessFilterAttribue());

        }
    }
}
