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
	internal class SharedContactForm : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Services.NewSchema.SharedContactForm>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Services.NewSchema.SharedContactForm> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Services.NewSchema.SharedContactForm), MappingDefaults.ServicesSchema);

			base.Configure(builder);
		}
	}
}
