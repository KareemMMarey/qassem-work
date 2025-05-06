using Framework.Core.Data;
using QassimPrincipality.Domain.Entities.Services.NewSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Lookups.NewSchema
{
	public class AttachmentType : LookupEntityBase
	{
		public bool IsMandatory { get; set; }
		public int MaxSizeMB { get; set; }
		public string AllowedExtensions { get; set; }
		public long? ServiceId { get; set; }

		public virtual EService EService { get; set; }
		public virtual ICollection<RequestAttachment> Attachments { get; set; }
	}
}
