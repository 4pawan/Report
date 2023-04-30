using Microsoft.AspNetCore.Mvc;

namespace Report.Web.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
