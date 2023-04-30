using Microsoft.AspNetCore.Mvc;
using Report.Web.Models;
using System.Diagnostics;
using Report.Web.EF;
using Report.Web.helper;

namespace Report.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment environment;
        private ApplicationDbContext dbContext;


        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _environment, ApplicationDbContext _applicationDbContext)
        {
            _logger = logger;
            environment = _environment;
            dbContext = _applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SyncMonthlyData()
        {
            string path = $@"{environment.WebRootPath}\input\m.json";
            var data = SyncDB.ReadMonthlyData(path , dbContext);
            
            return View("Index");
        }
        
        public IActionResult Sync5minData()
        {
            string path = $@"{environment.WebRootPath}\input\5min.json";
            var data = SyncDB.Sync5minData(path, dbContext);

            return View("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}