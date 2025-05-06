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
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string ShortDescriptionAr { get; set; }
		public string ShortDescriptionEn { get; set; }
		public string ImageUrl { get; set; }
		public DateTime PublishDate { get; set; }
		public bool IsPublished { get; set; }
	}
}
