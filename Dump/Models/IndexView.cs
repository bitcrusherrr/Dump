namespace Dump.Models
{
    using System.Collections.Generic;

    using Dump.Services;

    public class IndexView
    {
        private List<ImageItem> _images = new List<ImageItem>();

        public IndexView(List<ImageItem> images)
        {
            _images.AddRange(images);
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