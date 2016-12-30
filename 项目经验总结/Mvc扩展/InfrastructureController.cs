using System;
using System.Text;
using System.Web.Mvc;
using DTO;
using MvcCustommade.ControllerCommon;
using Newtonsoft.Json;

namespace MvcCustommade
{
    /// <summary>
    /// 可以在这里重写Controller类,达到替换Mvc组件的需求
    /// </summary>
    public class InfrastructureController : Controller
    {

        /// <summary>
        /// 封装上下文对象
        /// </summary>
        public WebWorkContext WorkContext = new WebWorkContext();

        /// <summary>
        /// 重写init方法
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            WorkContext.User = (UserDTO)System.Web.HttpContext.Current.Session["user"];
            // 判断用户信息
            if (WorkContext.User != null)
            {
                WorkContext.IsLogin = true;
                WorkContext.CloudId = WorkContext.User.CloudID;
                WorkContext.Name = WorkContext.User.name;
            }
            else
            {
                WorkContext.IsLogin = false;
            }

            // 判断是否是Get请求
            WorkContext.IsGet = requestContext.HttpContext.Request.HttpMethod == "GET";
            // 判断是否是Ajax请求
            WorkContext.IsAjax = requestContext.HttpContext.Request.IsAjaxRequest();

        }


        static InfrastructureController()
        {
            //全局设置时间格式化
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = () =>
            {
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                return setting;
            };
        }

        /// <summary>
        /// 将对象序列化并返回ContentResult类型
        /// </summary>
        /// <param name="data">被序列化的数据</param>
        /// <param name="properties">选择要返回的属性,默认全部序列化</param>
        /// /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        /// <param name="dateFormat">默认时间格式 yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public ContentResult JsonData(object data, string[] properties = null, bool retain = true, string dateFormat = null)
        {
            string result = null;
            LimitPropsContractResolver.CreateProperties(data, properties, retain);
            if (dateFormat != null)
            {
                var jsetting = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    DateFormatString = dateFormat

                };
                result = JsonConvert.SerializeObject(data, jsetting);
            }
            else
            {
                result = JsonConvert.SerializeObject(data);
            }


            return Content(result, "application/json", Encoding.UTF8);
        }

    }
}
