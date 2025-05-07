using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
	public class RequestAdditionalData : LookupEntityBase
	{
		public Guid RequestId { get; set; }

		public string NationalId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string City { get; set; }
		public string District { get; set; }
        public int? CountryId { get; set; }
        public int? PrisonFromId { get; set; }
		public int? PrisonToId { get; set; }
		public int? OtherDDLId { get; set; }
		public string RequestDetails { get; set; }

		public virtual ServiceRequest Request { get; set; }
		public virtual Country Country { get; set; }
		public virtual LookupOption PrisonFrom { get; set; }
		public virtual LookupOption PrisonTo { get; set; }
		public virtual LookupOption OtherDDL { get; set; }
	}
}
