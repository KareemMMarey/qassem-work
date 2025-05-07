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
	internal class AttachmentType : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.AttachmentType>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.AttachmentType> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.AttachmentType), MappingDefaults.LookupSchema);
			builder
			.HasOne(a => a.EService)
			.WithMany(s => s.AttachmentTypes)
			.HasForeignKey(a => a.ServiceId);
			base.Configure(builder);
		}
	}
}
