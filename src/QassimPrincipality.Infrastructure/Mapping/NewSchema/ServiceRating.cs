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
	internal class ServiceRating : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceRating>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceRating> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceRating), MappingDefaults.LookupSchema);

			builder
			.HasOne(r => r.EService)
			.WithMany(s => s.Ratings)
			.HasForeignKey(r => r.ServiceId);


			base.Configure(builder);
		}
	}
}
