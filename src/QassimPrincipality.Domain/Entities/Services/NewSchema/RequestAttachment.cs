using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Lookups.NewSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
	public class RequestAttachment : LookupEntityBase
	{
		public Guid RequestId { get; set; }
		public int AttachmentTypeId { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public DateTime UploadedAt { get; set; }
		public bool IsValid { get; set; }

		public virtual ServiceRequest Request { get; set; }
		public virtual AttachmentType AttachmentType { get; set; }

	}
}
