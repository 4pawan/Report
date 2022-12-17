using Microsoft.AspNetCore.Mvc;
using Report.Web.EF;

namespace Report.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController( ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: reportController1
        public ActionResult Index()
        {
            var list = _context.NiftyWeekly.OrderByDescending(d=>d.Date).AsEnumerable() ;
            return View(list);
        }

        // GET: reportController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: reportController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: reportController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: reportController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: reportController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: reportController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: reportController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
