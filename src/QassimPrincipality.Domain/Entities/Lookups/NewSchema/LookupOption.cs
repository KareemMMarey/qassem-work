using Framework.Core.Data;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class LookupOption : LookupEntityBase
	{
		public LookupOptionType LookupOptionType { get; set; }

	}
}
