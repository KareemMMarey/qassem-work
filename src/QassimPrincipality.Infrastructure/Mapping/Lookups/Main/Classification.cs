using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class Classification : EntityTypeConfiguration<Domain.Entities.Lookups.Main.Classification>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.Classification> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.Classification), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}