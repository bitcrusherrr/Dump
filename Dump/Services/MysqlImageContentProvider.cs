namespace Dump.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Dump.Models;

    public class MysqlImageContentProvider : IContentProvider
    {
        public ImageItem GetSpecificImage(string filename)
        {
            throw new NotImplementedException();
        }

        public List<ImageItem> GetImages(int page, string path, int quantity = 10)
        {
            throw new NotImplementedException();
        }

        public int TotalImageCount(string path)
        {
            throw new NotImplementedException();
        }

        public List<ImageItem> GetImageByFilter(List<Tuple<FilterType, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public void UploadImageItem(HttpPostedFileBase[] uploadedItems, dynamic viewBag, HttpServerUtilityBase server)
        {
            throw new NotImplementedException();
        }
    }
}