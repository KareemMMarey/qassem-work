using Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class ServiceFAQ : LookupEntityBase
	{
		public int ServiceId { get; set; }
		public string AnswerAr { get; set; }
		public string AnswerEn { get; set; }
		public int OrderIndex { get; set; }

		public virtual EService EService { get; set; }

	}
}
