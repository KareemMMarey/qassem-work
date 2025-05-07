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
	internal class EService : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EService>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.EService> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.EService), MappingDefaults.LookupSchema);
			builder
			.HasOne(s => s.EServiceForm)
			.WithMany(f => f.EServices)
			.HasForeignKey(s => s.EServiceFormId);

			builder
			.HasOne(s => s.ServicesCategory)
			.WithMany(f => f.EServices)
			.HasForeignKey(s => s.CategoryId);

			base.Configure(builder);
		}
	}
}
