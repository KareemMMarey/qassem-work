using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class EServiceFlow : LookupEntityBase
	{
		public string DescriptionAr { get; set; }
		public string DescriptionEn { get; set; }
		public int ServiceId { get; set; }
		public virtual EService EService { get; set; }
	}
}
