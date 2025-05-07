using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Application.Dtos.Content
{
	public class StatisticDto
	{
		public int Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string Value { get; set; }
		public string IconClass { get; set; }
		public int OrderIndex { get; set; }
		public bool IsActive { get; set; }
	}
}
