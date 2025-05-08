using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class PageFeedback : LookupEntityBase
	{
		[Required]
		[StringLength(500)]
		public string PageUrl { get; set; }

		[Required]
		public int TotalUsers { get; set; } = 0;

		[Required]
		public int PositiveResponses { get; set; } = 0;

		[Required]
		public int NegativeResponses { get; set; } = 0;
	}
}
