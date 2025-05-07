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
	internal class RequestBasicData : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestBasicData>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestBasicData> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Services.NewSchema.RequestBasicData), MappingDefaults.ServicesSchema);
			builder.HasOne(b => b.Request)
			.WithOne(r => r.BasicData)
			.HasForeignKey<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestBasicData>(b => b.RequestId);
			base.Configure(builder);
		}
	}
}
