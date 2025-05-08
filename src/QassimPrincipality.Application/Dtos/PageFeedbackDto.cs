using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos
{
	public class PageFeedbackDto
	{
		public long Id { get; set; }
		public string PageUrl { get; set; }
		public int TotalUsers { get; set; }
		public int PositiveResponses { get; set; }
		public int NegativeResponses { get; set; }
		public DateTime CreatedAt { get; set; }
		public int PositivePercentage => TotalUsers > 0 ? (PositiveResponses * 100) / TotalUsers : 0;
	}
}
