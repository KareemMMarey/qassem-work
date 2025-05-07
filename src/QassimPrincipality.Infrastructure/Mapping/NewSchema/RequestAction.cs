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
	internal class RequestAction : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAction>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAction> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Services.NewSchema.RequestAction), MappingDefaults.ServicesSchema);
			builder
			.HasOne(a => a.Request)
			.WithMany(r => r.Actions)
			.HasForeignKey(a => a.RequestId);
			base.Configure(builder);
		}
	}
}
