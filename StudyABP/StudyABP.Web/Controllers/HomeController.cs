using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace StudyABP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : StudyABPControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}