using Framework.Core.Data;
using QassimPrincipality.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Domain.Entities.Services.NewSchema
{
	public class SharedContactForm : LookupEntityBase
	{
		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; }

		[NotMapped]
		public string FullName => $"{FirstName} {LastName}";

		[Required]
		[EmailAddress]
		[MaxLength(200)]
		public string Email { get; set; }

		[Required]
		public ContactMessageType MessageType { get; set; }

		[Required]
		[MaxLength(200)]
		public string Subject { get; set; }

		[Required]
		public string Message { get; set; }
	}
}
