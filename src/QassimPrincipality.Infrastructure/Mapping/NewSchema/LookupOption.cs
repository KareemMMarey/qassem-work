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
	internal class LookupOption : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.LookupOption>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.LookupOption> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.LookupOption), MappingDefaults.LookupSchema);
			builder.Property(s => s.LookupOptionType).HasConversion<string>();
			base.Configure(builder);
		}
	}
}
