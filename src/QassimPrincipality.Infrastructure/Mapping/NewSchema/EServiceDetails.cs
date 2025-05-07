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
	internal class EServiceDetails : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceDetails>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceDetails> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceDetails), MappingDefaults.LookupSchema);

			builder
			.HasOne(d => d.EService)
			.WithOne(s => s.EServiceDetails)
			.HasForeignKey<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EServiceDetails>(d => d.ServiceId);

			base.Configure(builder);
		}
	}
}
