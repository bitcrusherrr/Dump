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

	public class HomeController : Controller
    {
		private ImageContentProvider _imageSource = new ImageContentProvider();
        public ActionResult Index(int page = 0)
        {
            return View(new IndexView(_imageSource.GetImages(0, Server.MapPath(string.Format("~/UploadedData/Images")))));
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

        public ActionResult Upload()
        {
            return View();
        }

		[HttpPost]
		public ActionResult HandleFileUpload(HttpPostedFileBase[] files)
		{
			try
			{
				foreach (var file in files)
				{
					if (file != null && file.ContentLength > 0)
					{
						// Get file info
						var fileName = Path.GetFileName(file.FileName);
						var contentLength = file.ContentLength;
						var contentType = file.ContentType;

						// Get file data
						byte[] data = new byte[] { };
						using (var binaryReader = new BinaryReader(file.InputStream))
						{
							data = binaryReader.ReadBytes(file.ContentLength);
						}

						string hashName = string.Empty;
						using (MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider())
						{
							byte[] checksum = new MD5CryptoServiceProvider().ComputeHash(data);
							hashName = (BitConverter.ToString(checksum).Replace("-", string.Empty)).ToLower();
						}
						var path = Server.MapPath(string.Format("~/UploadedData/Images/{0}{1}", hashName, Path.GetExtension(file.FileName)));
						System.IO.File.WriteAllBytes(path, data);

						var image = ScaleImage(Image.FromFile(path), 200);

						path = Server.MapPath(string.Format("~/UploadedData/Images/Thumbs/{0}{1}", hashName, Path.GetExtension(file.FileName)));
						image.Save(path);
					}
				}

				ViewBag.Message = "File(s) Uploaded successfully.";
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Something went wrong...";
				Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
			}

			return View("Upload");
		}

		public Image ScaleImage(System.Drawing.Image image, int maxHeight)
		{
			var ratio = (double)maxHeight / image.Height;
			var newWidth = (int)(image.Width * ratio);
			var newHeight = (int)(image.Height * ratio);
			var newImage = new Bitmap(newWidth, newHeight);
			using (var g = Graphics.FromImage(newImage))
			{
				g.DrawImage(image, 0, 0, newWidth, newHeight);
			}
			return newImage;
		}
	}
}