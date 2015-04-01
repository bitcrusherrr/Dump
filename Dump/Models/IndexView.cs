namespace Dump.Models
{
    using System.Collections.Generic;

    using Dump.Services;

    public class IndexView
    {
        private List<ImageItem> _images = new List<ImageItem>();
        private int page;

        public IndexView(int page)
        {
            this.page = page;

            _images.AddRange(new ImageContentProvider().GetImages(page));
        }

        public List<ImageItem> Images
        {
            get
            {
                return _images;
            } 
            
        }
    }
}