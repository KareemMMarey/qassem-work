using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class EntityType : EntityTypeConfiguration<Domain.Entities.Lookups.Main.EntityType>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.EntityType> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.EntityType), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}