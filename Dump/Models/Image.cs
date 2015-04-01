using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dump.Models
{
    public class ImageItem
    {
        public string FileName { get; set; }

		public string ThumbFileName { get; set; }

		public string Class
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(FileName) && FileName.Contains("gif"))
				{
					return "img-thumbnail-gif";
                }
				else
				{
					return "img-thumbnail";
				}
			}
		}
	}
}