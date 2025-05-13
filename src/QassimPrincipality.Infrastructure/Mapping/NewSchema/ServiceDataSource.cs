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
    internal class ServiceDataSource : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceDataSource>
    {
        public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceDataSource> builder)
        {
            builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceDataSource), MappingDefaults.LookupSchema);

            builder
            .HasOne(d => d.ServiceStep)
            .WithMany(s => s.DataSources)
            .HasForeignKey(d => d.StepId);

            base.Configure(builder);
        }
    }
}
