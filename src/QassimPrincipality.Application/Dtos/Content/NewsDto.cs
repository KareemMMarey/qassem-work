using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
	public class NewsDto
	{
		public int Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string ShortDescriptionAr { get; set; }
		public string ShortDescriptionEn { get; set; }
		public string ImageUrl { get; set; }
		public string ImageThumbnailUrl { get; set; }
		public DateTime PublishDate { get; set; }
		public string PublishDateString { get; set; }
		public bool IsPublished { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
