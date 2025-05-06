using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class ServiceTab : LookupEntityBase
	{
		public int ServiceId { get; set; }
		public string TabType { get; set; }
		public string ContentAr { get; set; }
		public string ContentEn { get; set; }
		public int OrderIndex { get; set; }
		public virtual EService EService { get; set; }

	}
}
