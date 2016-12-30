using DTO.UserRole.Input;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tool;

namespace MvcCustommade
{
    /// <summary>
    /// Model参数验证拦截器
    /// </summary>
    public class ModelValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var con = filterContext.Controller as Controller;
            if (!con.ModelState.IsValid)
            {
                string errorMessage = "服务器繁忙,请稍后再试";
                
                foreach (var value in con.ModelState.Values)
                {
                    if (!value.Errors.AsEnumerable().Any()) continue;

                    errorMessage = value.Errors.LastOrDefault().ErrorMessage;
                    break;
                }

                filterContext.Result = new JsonResult() { Data = ResponseManager.GetOutputResponse(OutputStatus.NoParameter, errorMessage), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
    }
}
