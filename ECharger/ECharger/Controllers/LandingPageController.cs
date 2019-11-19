using System.Web.Mvc;

namespace ECharger.Controllers
{
    [AllowAnonymous]
    public class LandingPageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}