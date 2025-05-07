using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Infrastructure.Mapping.NewSchema
{
	internal class RequestAttachment : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAttachment>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAttachment> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAttachment), MappingDefaults.ServicesSchema);
			builder
			.HasOne(a => a.Request)
			.WithMany(r => r.Attachments)
			.HasForeignKey(a => a.RequestId);

			builder
			.HasOne(a => a.AttachmentType)
			.WithMany(t => t.Attachments)
			.HasForeignKey(a => a.AttachmentTypeId);

			base.Configure(builder);
		}
	}
}
