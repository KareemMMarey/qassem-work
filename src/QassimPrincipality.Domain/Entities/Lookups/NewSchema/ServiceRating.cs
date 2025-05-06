using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class ServiceRating : LookupEntityBase
	{
		public int ServiceId { get; set; }
		public int RatingValue { get; set; }
		public string UserId { get; set; }
		public virtual EService EService { get; set; }
	}
}
