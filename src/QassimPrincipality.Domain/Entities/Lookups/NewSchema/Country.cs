using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class Country : LookupEntityBase
	{
		public string NationalityAr { get; set; }

		public string NationalityEn { get; set; }
	}
}
