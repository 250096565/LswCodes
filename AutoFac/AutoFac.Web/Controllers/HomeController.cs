using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoFac.EF;
using Core.User;

namespace AutoFac.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            using (Entites aContext = new Entites())
            {
                #region 乐观锁玩法
                //var firstOrDefault = aContext.User.FirstOrDefault();
                //firstOrDefault.Name = "小明";

                //using (Entites bContext = new Entites())
                //{
                //    var firstOrDefault2 = bContext.User.FirstOrDefault();
                //    firstOrDefault2.Name = "小红";
                //    bContext.SaveChanges();
                //}
                //try
                //{
                //    aContext.SaveChanges();
                //}
                //catch (Exception exception)
                //{

                //    throw new Exception(exception.Message);
                //} 
                #endregion

                Core.User.User user = new User()
                {
                    Name = "小明",
                    UserAddress = new UserAddress() { City = "北京", DynamicAddress = "北京知春路" }
                };

                aContext.User.Add(user);

                Core.User.SuperUser superUser = new SuperUser()
                {
                    Name = "超级小明",
                    UserNum = "12346789",
                    UserAddress = new UserAddress() { City = "北京", DynamicAddress = "北京798艺术区" }
                };
                aContext.User.Add(superUser);

                aContext.SaveChanges();

            }
            return View();
        }
    }
}