using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
	public class PageFeedbackSearchDto
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string PageUrl { get; set; }
		public int TotalItemsCount { get; set; }
		public StaticPagedList<PageFeedbackDto> Items { get; set; }
	}
}
