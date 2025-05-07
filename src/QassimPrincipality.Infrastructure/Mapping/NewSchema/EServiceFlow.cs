using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QassimPrincipality.Infrastructure.Mapping.NewSchema
{
	internal class EServiceFlow : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceFlow>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceFlow> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceFlow), MappingDefaults.LookupSchema);

			builder
			.HasOne(d => d.EService)
			.WithMany(s => s.EServiceFlows)
			.HasForeignKey(d => d.ServiceId);

			base.Configure(builder);
		}
	}
}
