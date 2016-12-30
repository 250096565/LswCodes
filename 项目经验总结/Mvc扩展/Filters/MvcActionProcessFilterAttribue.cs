using Exceptionless;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Configuration;
using System.Linq;
using System.Web.Hosting;

namespace MvcCustommade.Filters
{
    /// <summary>
    /// Mvc action执行前后处理
    /// </summary>
    public class MvcActionProcessFilterAttribue : ActionFilterAttribute
    {

        private static XDocument XDocument { get; set; }


        static MvcActionProcessFilterAttribue()
        {
            var proejctName = ConfigurationManager.AppSettings["ProjectName"] as string;
            //拿到包含注释的xml文档
            XDocument = XDocument.Load(HostingEnvironment.MapPath(@"~/App_Data/" + proejctName + ".XML"));
        }

        /// <summary>
        /// 方法执行后进行的操作
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                //记录审记日志  
                //拿到当前登录人的云码
                var con = filterContext.Controller as InfrastructureController;
                var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var actionName = filterContext.ActionDescriptor.ActionName;
                //拿到方法上的注释
                //拿到方法上的注释没有参数的方法没有(括号
                var summary = (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName + "(") select member.Element("summary").Value).FirstOrDefault() ??
                             (from member in XDocument.Elements("doc").Elements("members").Elements("member") where member.Attribute("name").Value.ToString().Contains("." + controllerName + "Controller." + actionName) select member.Element("summary").Value).FirstOrDefault();

                ExceptionlessClient.Default.CreateFeatureUsage(actionName + "   " + summary)
                    .AddTags(controllerName, con.WorkContext.CloudId)
                    .Submit();
            }
            catch
            {
                return;
            }


        }
    }
}
