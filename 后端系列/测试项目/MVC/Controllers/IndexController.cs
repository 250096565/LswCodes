using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace MVC.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (var entities = new SAHZSQEntities())
            {
                var result = entities.UserRole.Where(o => true).Take(5000).ToList();
                var result1 = entities.User.Where(o => true).Take(5000).ToList();
            }

            watch.Stop();

            ViewBag.time = watch.ElapsedMilliseconds;
            return View();
        }
    }
}