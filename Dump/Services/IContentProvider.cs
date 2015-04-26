namespace Dump.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Dump.Models;

    interface IContentProvider
    {
        ImageItem GetSpecificImage(string filename);

        List<ImageItem> GetImages(int page, string path, int quantity = 10);

        int TotalImageCount(string path);

        List<ImageItem> GetImageByFilter(List<Tuple<FilterType, string>> parameters);

        void UploadImageItem(HttpPostedFileBase[] uploadedItems, dynamic ViewBag, HttpServerUtilityBase Server);
		List<ImageItem> GetRelatedImages(string item);
	}
}
