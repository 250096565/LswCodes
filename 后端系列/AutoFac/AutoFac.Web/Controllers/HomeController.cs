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

            using (var aContext = new Entites())
            {
                #region 乐观锁玩法
                //var firstOrDefault = aContext.User.AsNoTracking().FirstOrDefault();
                //firstOrDefault.Name = "小明";

                //using (var bContext = new Entites())
                //{
                //    var firstOrDefault2 = bContext.User.AsNoTracking().FirstOrDefault();

                //    firstOrDefault2.Name = "小红";
                //    bContext.Entry(firstOrDefault2).State = EntityState.Modified;
                //    bContext.SaveChanges();
                //}
                //try
                //{
                //    aContext.Entry(firstOrDefault).State = EntityState.Modified;
                //    aContext.SaveChanges();
                //}
                //catch (Exception exception)
                //{
                //    throw new Exception(exception.Message);
                //}
                #endregion

                #region 准备数据 
                Core.User.User user = new User()
                {
                    Name = "小明",
                    Address = new UserAddress() { City = "北京", DynamicAddress = "北京知春路" }

                };
                user.Card = new UserCard() { IdCard = "19561816189161561" };

                aContext.User.Add(user);


                Core.User.SuperUser superUser = new SuperUser()
                {
                    Name = "超级小明",
                    UserNum = "12346789",
                    Address = new UserAddress() { City = "北京", DynamicAddress = "北京798艺术区" }
                };

                aContext.User.Add(superUser);

                aContext.SaveChanges();


                var firstuser = aContext.User.FirstOrDefault();
                var superuser = aContext.SuperUser.FirstOrDefault();
                #endregion

            }
            return View();
        }
    }
}