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
	internal class ServiceFAQ : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceFAQ>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceFAQ> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceFAQ), MappingDefaults.LookupSchema);

			builder
		   .HasOne(f => f.EService)
		   .WithMany(s => s.FAQs)
		   .HasForeignKey(f => f.ServiceId);

			base.Configure(builder);
		}
	}
}
