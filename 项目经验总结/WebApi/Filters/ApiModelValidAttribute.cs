using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using Tool;

namespace RestfulApi.Filters
{
    public class ApiModelValidAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 验证参数合法性
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {
                string errorMessage = "服务器繁忙,请稍后再试";
                foreach (var value in actionContext.ModelState.Values)
                {
                    if (!value.Errors.AsEnumerable().Any()) continue;

                    errorMessage = value.Errors.LastOrDefault().ErrorMessage;

                    break;
                }
                actionContext.Response = ResponseHelper.CreatHttpResponseMessage(ResponseManager.GetOutputResponse(OutputStatus.ErrorParameter, errorMessage));
            }
        }
    }
}