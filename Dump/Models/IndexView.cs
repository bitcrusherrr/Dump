namespace Dump.Models
{
    using System.Collections.Generic;

    using Dump.Services;

    public class IndexView
    {
        private List<ImageItem> _images = new List<ImageItem>();

        public IndexView(List<ImageItem> images, int totalCount, int perPage)
        {
            _images.AddRange(images);
			Pages = 0;

			while (totalCount > 0)
			{
				Pages++;
				totalCount = totalCount - perPage;
            }
        }

        public List<ImageItem> Images
        {
            get
            {
                return _images;
            } 
            
        }

		public int Pages
		{
			get; private set;
		}
    }
}