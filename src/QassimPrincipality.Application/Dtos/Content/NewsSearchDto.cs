using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
	public class NewsSearchDto
	{
		public string? Title { get; set; }
		public DateTime? PublishDateFrom { get; set; }
		public DateTime? PublishDateTo { get; set; }
		public bool? IsPublished { get; set; }

		public int PageNumber { get; set; } = 1;
		public int? PageSize { get; set; }

		public IPagedList<NewsDto>? Items { get; set; }
		public int TotalItemsCount { get; set; }
	}

}
