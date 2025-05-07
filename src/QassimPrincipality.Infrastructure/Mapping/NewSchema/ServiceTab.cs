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
	internal class ServiceTab : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceTab>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceTab> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceTab), MappingDefaults.LookupSchema);
			builder.Property(s => s.TabType).HasConversion<string>();
			builder
			.HasOne(t => t.EService)
			.WithMany(s => s.Tabs)
			.HasForeignKey(t => t.ServiceId);

			base.Configure(builder);
		}
	}
}
