using DTO;
using Enums;
using Manager;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Tool;
using Tool.Response;
namespace RestfulApi.Filters
{
    /// <summary>
    /// api安全验证filter
    /// </summary>
    public class ApiValidFilterAttribute : ActionFilterAttribute
    {
        public readonly Source[] AllSource;

        [ThreadStatic]
        public static Source Source;

        [ThreadStatic]
        public static TokenDTO TokenDto;

        /// <summary>
        /// 版本号
        /// </summary>
        [ThreadStatic]
        public static string objVersion;

        /// <summary>
        /// 
        /// </summary>
        public readonly ValidType ValidType;

        /// <summary>
        /// 传入api允许请求来源
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="validType">验证方式</param>
        public ApiValidFilterAttribute(Source[] sources, ValidType validType = ValidType.Valid)
        {
            this.AllSource = sources;
            this.ValidType = validType;
        }



        /// <summary>
        /// action执行前执行此方法
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpContext context = HttpContext.Current;


            //验证来源 token 与签名   如果路由中没有就从header中拿
            string objSource = actionContext.ControllerContext.RouteData.Values["source"] as string;
            if (string.IsNullOrWhiteSpace(objSource))
                objSource = context.Request.Headers["source"];
            objVersion = actionContext.ControllerContext.RouteData.Values["version"] as string;
            if (string.IsNullOrWhiteSpace(objVersion))
                objVersion = context.Request.Headers["version"];

            //拿到控制器名称
            string controllerName = "";
            if (actionContext.ControllerContext.RouteData.Values.ContainsKey("controller"))
                controllerName = actionContext.ControllerContext.RouteData.Values["controller"] as string;


            //拿到通用参数  如果路由中没有就从header中拿
            string token = context.Request["token"];
            if (string.IsNullOrWhiteSpace(token))
                token = context.Request.Headers["token"];
            string timestamp = context.Request["timestamp"];
            if (string.IsNullOrWhiteSpace(timestamp))
                timestamp = context.Request.Headers["timestamp"];
            string signature = context.Request["signature"];
            if (string.IsNullOrWhiteSpace(signature))
                signature = context.Request.Headers["signature"];
            OutputModel outputModel = null;

            switch (ValidType)
            {
                case ValidType.Valid:
                    if ((outputModel =
                RequestManager.VerificationSource(out TokenDto, objVersion, objSource, token, timestamp, signature,
                    out Source,
                    AllSource, controllerName)) == null)
                        return;
                    break;
                case ValidType.NoVliad:
                    return;
                case ValidType.AllSourceAndNoValid:
                    if (!Enum.TryParse<Source>(objSource, true, out Source))
                        outputModel = ResponseManager.GetOutputResponse(OutputStatus.ErrorSource);
                    return;
                case ValidType.AllSource:
                    var allSource = Source.ToList<Source>();
                    if ((outputModel =
                  RequestManager.VerificationSource(out TokenDto, objVersion, objSource, token, timestamp, signature,
                      out Source,
                      allSource.ToArray(), controllerName)) == null)
                        return;
                    break;

            }

            if (actionContext.Request.Method.ToString().ToLower().Equals("post") &&
                actionContext.ActionArguments.Count <= 0)
            {
                //是上传资源的post提交
                ApiActionProcessFilterAttribute.PostActionExceuted(new Dictionary<string, object>() { { "token", token }, { "timestamp", timestamp }, { "signature", signature } }, actionContext);
            }
            actionContext.Response = ResponseHelper.CreatHttpResponseMessage(outputModel);
        }


    }

    /// <summary>
    /// 
    /// </summary>
    public enum ValidType
    {
        /// <summary>
        /// 所有的来源
        /// </summary>
        AllSource = 1,

        /// <summary>
        /// 不进行验证
        /// </summary>
        NoVliad = 2,

        /// <summary>
        /// 所有的来源并且不进行验证
        /// </summary>
        AllSourceAndNoValid = 3,

        /// <summary>
        /// 需要验证
        /// </summary>
        Valid = 4

    }




}