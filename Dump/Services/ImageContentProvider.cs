namespace Dump.Services
{
	using System;
	using System.Collections.Generic;

	using Dump.Models;
	using System.Web;
	using System.IO;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Drawing;
	using Dump.Properties;

	public class ImageContentProvider : IContentProvider
    {
		string[] extensions = Settings.Default.AllowedExtensions.Split(',');
		public ImageItem GetSpecificImage(string filename)
        {
			return new ImageItem() { FileName = string.Format("~/UploadedData/Images/{0}", filename), ThumbFileName = string.Format("~/UploadedData/Images/Thumbs/{0}", filename) };
        }

        public List<ImageItem> GetImages(int page, string path, int quantity = 10)
        {
			var result = new List<ImageItem>();
			foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => extensions.Any(ext => ext == Path.GetExtension(s))))
			{
				result.Add(new ImageItem() { FileName = Path.Combine("~/UploadedData/Images/", Path.GetFileName(file)), 
                    ThumbFileName = Path.Combine("~/UploadedData/Images/Thumbs/", Path.GetFileName(file)),
                    DateUploaded = File.GetCreationTime(file)
                });
            }

            result = result.OrderBy(x => x.DateUploaded).Reverse().ToList();

			if (result.Count() > quantity)
			{
				var pages = new List<ImageItem>();
				int counter = 0;
				for (int i = quantity * page; i < result.Count() && counter < quantity; i++)
				{
					counter++;
					pages.Add(result[i]);
				}

				return pages;
			}
			else
			{
				return result;
			}
        }

		/// <summary>
		/// Test interface!
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public List<ImageItem> GetRelatedImages(string item)
		{
			return new List<ImageItem>()
			{
				new ImageItem() { FileName = string.Format("~/UploadedData/Images/{0}", item), ThumbFileName = string.Format("~/UploadedData/Images/Thumbs/{0}", item) }
			};
        }

		public int TotalImageCount(string path)
		{
			return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Count(s => extensions.Any(ext => ext == Path.GetExtension(s)));
        }

		public List<ImageItem> GetImageByFilter(List<Tuple<FilterType,string>> parameters)
        {
            throw new NotImplementedException("Oi m8");
        }


        public void UploadImageItem(HttpPostedFileBase[] uploadedItems, dynamic ViewBag, HttpServerUtilityBase Server)
		{
            foreach (var file in uploadedItems)
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
                    var path = Server.MapPath(string.Format("/UploadedData/Images/{0}{1}", hashName, Path.GetExtension(file.FileName)));

                    if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.WriteAllBytes(path, data);

                        var image = ScaleImage(Image.FromFile(path), 200);

                        path = Server.MapPath(string.Format("/UploadedData/Images/Thumbs/{0}{1}", hashName, Path.GetExtension(file.FileName)));
                        image.Save(path);

                        ViewBag.Message += fileName + " Uploaded successfully." + Environment.NewLine;
                    }
                    else
                    {
                        ViewBag.Message += fileName + " already exists." + Environment.NewLine;
                    }
                }
            }
		}

        private Image ScaleImage(System.Drawing.Image image, int maxHeight)
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