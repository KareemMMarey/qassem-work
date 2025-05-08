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
	internal class PageFeedback : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.PageFeedback>
	{
		public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.PageFeedback> builder)
		{
			builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.PageFeedback), MappingDefaults.LookupSchema);

			base.Configure(builder);
		}
	}
}
