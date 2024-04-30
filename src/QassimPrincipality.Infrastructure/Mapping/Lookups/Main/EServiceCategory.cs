using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class EServiceCategory : EntityTypeConfiguration<Domain.Entities.Lookups.Main.EServiceCategory>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.EServiceCategory> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.EServiceCategory), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}