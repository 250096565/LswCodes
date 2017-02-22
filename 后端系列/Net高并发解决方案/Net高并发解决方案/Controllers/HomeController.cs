using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EF;
using Hangfire;
using Net高并发解决方案.Models;
using Net高并发解决方案.RabbitMq;

namespace Net高并发解决方案.Controllers
{
    public class HomeController : Controller
    {
        // GET: Controller
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 抢单接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GrabSingle(User user)
        {
            //使用后台任务
            //BackgroundJob.Enqueue(() => MqPublish.AddQueue(user));
            MqPublish.AddQueue(user);
            //MqPublish.AddQueue(user);
            return Json(new { Status = "OK" });
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetCount()
        {
            using (var dbcontext = new Model1())
            {
                return Json(new { Count = await dbcontext.Person.CountAsync() }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}