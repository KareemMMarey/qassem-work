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
	internal class ServicesCategory : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServicesCategory>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServicesCategory> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServicesCategory), MappingDefaults.LookupSchema);

			base.Configure(builder);
		}
	}
}
