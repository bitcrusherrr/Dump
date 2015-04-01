namespace Dump.Controllers
{
    using System.Web.Mvc;

    using Dump.Models;
    using Dump.Services;

    public class HomeController : Controller
    {
        public ActionResult Index(int page = 0)
        {
            return View(new IndexView(page));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Image(ImageItem item)
        {
            return View(item);
        }

        public ActionResult Upload()
        {
            return View();
        }
    }
}