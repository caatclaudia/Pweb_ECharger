using System.Web.Mvc;

namespace ECharger.Controllers
{
    public class LandingPageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}