using System.Web.Mvc;

namespace InsuranceCompany.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [System.Web.Http.Authorize(Roles = "User, Admin")]
        public ActionResult Company()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
