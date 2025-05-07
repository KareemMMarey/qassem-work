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
	internal class ServiceRequest : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Services.NewSchema.ServiceRequest>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Services.NewSchema.ServiceRequest> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Services.NewSchema.ServiceRequest), MappingDefaults.ServicesSchema);
			builder.Property(s => s.Status).HasConversion<string>();
			builder.Property(s => s.ServiceRequesterRelation).HasConversion<string>();

			builder.HasOne(sr => sr.EService)
			.WithMany(s => s.Requests)
			.HasForeignKey(sr => sr.ServiceId);


			base.Configure(builder);
		}
	}
}
