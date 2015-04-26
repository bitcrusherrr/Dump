namespace Dump.Controllers
{
	using System.Web.Mvc;

	using Dump.Models;
	using Dump.Services;
	using System.IO;
	using System.Drawing;
	using System.Security.Cryptography;
	using System;
	using System.Web;
	using System.Collections.Generic;
	using Dump.Properties;

	public class HomeController : Controller
    {
        private IContentProvider _imageSource = new ImageContentProvider();

        public ActionResult Index(int page = 0)
        {
            return View(new IndexView(_imageSource.GetImages(page, Server.MapPath(string.Format("~/UploadedData/Images")), Settings.Default.ItemsPerPage), _imageSource.TotalImageCount(Server.MapPath(string.Format("~/UploadedData/Images"))), Settings.Default.ItemsPerPage));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ImageView(string item)
        {
            return View(_imageSource.GetSpecificImage(item));
        }

		public ActionResult ImageViewPartial(string item)
		{
			return PartialView(new IndexView(_imageSource.GetRelatedImages(item), 1,1));
		}

		public ActionResult Upload()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase[] files)
		{
			try
			{
                _imageSource.UploadImageItem(files, ViewBag, Server);
				
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Something went wrong...";
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
			}

			return View();
		}
	}
}