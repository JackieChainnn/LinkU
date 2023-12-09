using Microsoft.AspNetCore.Mvc;

namespace LinkU.Areas.Company.Controllers
{
    [Area("Company")]
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

    }
}
