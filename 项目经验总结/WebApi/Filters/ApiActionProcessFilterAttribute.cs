using System.Linq;
using System.Web.Http.Filters;
using Exceptionless;
using System.Xml.Linq;
using System.Web.Hosting;
using System.Text;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace RestfulApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiActionProcessFilterAttribute : ActionFilterAttribute
    {

        private static XDocument XDocument { get; set; }


        static ApiActionProcessFilterAttribute()
        {
            XDocument = XDocument.Load(HostingEnvironment.MapPath(@"~/App_Data/RestfulApi.XML"));
        }


        /// <summary>
        /// action执行后执行此方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //拿到包含注释的xml文档
            try
            {
                var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                //拿到方法上的注释没有参数的方法没有(括号
                var summary = (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName + "(") select member.Element("summary").Value).FirstOrDefault() ??
                             (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName) select member.Element("summary").Value).FirstOrDefault();

                var list = actionExecutedContext.ActionContext.ActionArguments;

                if (actionExecutedContext.Request.Method.ToString().ToLower().Equals("post") && list.Count <= 0)
                    //post上传请求,过滤掉
                    return;

                var parame = new StringBuilder();

                foreach (var temp in list)
                {
                    if (actionExecutedContext.ActionContext.Request.Method.ToString().ToLower().Equals("post"))
                    {

                        var model = temp.Value;

                        var type = model.GetType();

                        foreach (var proerty in type.GetProperties())
                        {
                            var value = proerty.GetValue(model);

                            parame.Append("  " + proerty.Name + ": " + (value == null ? "" : value.ToString()) + "      ||");
                        }
                        break;

                    }
                    parame.Append("  " + temp.Key + ": " + (temp.Value == null ? "" : temp.Value.ToString()) + "      ||");

                }

                ExceptionlessClient.Default.CreateFeatureUsage(actionName + "   " + summary)
                    .AddTags(controllerName,
                        ApiValidFilterAttribute.TokenDto == null ? "" : ApiValidFilterAttribute.TokenDto.cloudid,
                        ApiValidFilterAttribute.Source.ToString(), parame.ToString())
                    .Submit();
            }
            catch
            {
                return;
            }

        }


        /// <summary>
        /// post上传action审计日志方法
        /// </summary>
        /// <param name="dict">参数字典</param>
        /// <param name="actionContext">上下文对象</param>
        public static void PostActionExceuted(Dictionary<string, object> dict, HttpActionContext actionContext)
        {
            //拿到包含注释的xml文档

            try
            {
                var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var actionName = actionContext.ActionDescriptor.ActionName;
                //拿到方法上的注释,没有参数的方法没有(括号
                var summary = (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName + "(") select member.Element("summary").Value).FirstOrDefault() ??
                              (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName) select member.Element("summary").Value).FirstOrDefault();
                var parame = new StringBuilder();
                foreach (var temp in dict)
                {
                    parame.Append("  " + temp.Key + ": " + (temp.Value == null ? "" : temp.Value.ToString()) + "      ||");
                }

                ExceptionlessClient.Default.CreateFeatureUsage(actionName + "   " + summary)
                    .AddTags(controllerName,
                        ApiValidFilterAttribute.TokenDto == null ? "" : ApiValidFilterAttribute.TokenDto.cloudid,
                        ApiValidFilterAttribute.Source.ToString(), parame.ToString())
                    .Submit();
            }
            catch
            {
                return;
            }

        }

    }
}