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
    internal class ServiceStep : EntityTypeConfiguration<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceStep>
    {
        public override void Configure(EntityTypeBuilder<QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceStep> builder)
        {
            builder.ToTable(nameof(QassimPrincipality.Domain.Entities.Lookups.NewSchema.ServiceStep), MappingDefaults.LookupSchema);

            builder
            .HasOne(d => d.EService)
            .WithMany(s => s.ServiceSteps)
            .HasForeignKey(d => d.ServiceId);

            base.Configure(builder);
        }
    }
}
