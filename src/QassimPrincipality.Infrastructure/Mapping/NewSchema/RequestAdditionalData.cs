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
	internal class RequestAdditionalData : EntityTypeConfiguration<Domain.Entities.Services.NewSchema.RequestAdditionalData>
	{
		public override void Configure(EntityTypeBuilder<Domain.Entities.Services.NewSchema.RequestAdditionalData> builder)
		{
			builder.ToTable(nameof(Domain.Entities.Services.NewSchema.RequestAdditionalData), MappingDefaults.ServicesSchema);
			builder.HasOne(b => b.Request)
		   .WithOne(r => r.AdditionalData)
		   .HasForeignKey<Domain.Entities.Services.NewSchema.RequestAdditionalData>(b => b.RequestId);


			builder
			.HasOne(r => r.PrisonFrom)
			.WithMany(l => l.RequestAdditionalDataPrisonFrom)
			.HasForeignKey(r => r.PrisonFromId)
			.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(r => r.PrisonTo)
				.WithMany(l => l.RequestAdditionalDataPrisonTo)
				.HasForeignKey(r => r.PrisonToId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(r => r.OtherDDL)
				.WithMany(l => l.RequestAdditionalOtherData)
				.HasForeignKey(r => r.OtherDDLId)
				.OnDelete(DeleteBehavior.Restrict);

			base.Configure(builder);
		}
	}
}
