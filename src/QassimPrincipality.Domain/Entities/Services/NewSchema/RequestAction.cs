using Framework.Core.Data;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
	public class RequestAction : LookupEntityBase
	{
		public Guid RequestId { get; set; }                // FK to ServiceRequest
		public ServiceRequestStatus FromStatus { get; set; }
		public ServiceRequestStatus ToStatus { get; set; }

		public string ActionBy { get; set; }               // User who performed the action
		public DateTime ActionDate { get; set; }

		public string? Reason { get; set; }                // Optional: for approval or rejection
		public string? Notes { get; set; }

		public virtual ServiceRequest Request { get; set; }

	}
}
