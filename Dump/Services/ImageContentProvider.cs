namespace Dump.Services
{
	using System;
	using System.Collections.Generic;

	using Dump.Models;
	using System.Web;
	using System.IO;
	using System.Linq;
	public class ImageContentProvider
    {
		string[] extensions = { ".jpg", ".png", ".gif", ".jpeg" };

		public ImageItem GetSpecificImage(string filename)
        {
			return new ImageItem() { FileName = string.Format("~/UploadedData/Images/{0}", filename), ThumbFileName = string.Format("~/UploadedData/Images/Thumbs/{0}", filename) };
        }

        public List<ImageItem> GetImages(int page, string path, int quantity = 10)
        {
			var result = new List<ImageItem>();
			foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => extensions.Any(ext => ext == Path.GetExtension(s))))
			{
				result.Add(new ImageItem() { FileName = Path.Combine("~/UploadedData/Images/", Path.GetFileName(file)), ThumbFileName = Path.Combine("~/UploadedData/Images/Thumbs/", Path.GetFileName(file)) });
            }

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

		internal int TotalImageCount(string path)
		{
			return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => extensions.Any(ext => ext == Path.GetExtension(s))).Count();
        }

		public List<ImageItem> GetImageByFilter()
        {
            throw new NotImplementedException("Oi m8");
        }

		public void UploadImageItem(HttpFileCollection uploadedItems)
		{
			throw new NotImplementedException("Oi m8");
		}
    }
}