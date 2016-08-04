using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWebAPI.Controllers
{
    /// <summary>
    /// 主页面资源
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 跳转到主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {//aaaa
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
