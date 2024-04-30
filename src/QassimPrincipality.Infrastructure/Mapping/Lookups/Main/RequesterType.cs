using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class RequesterType : EntityTypeConfiguration<Domain.Entities.Lookups.Main.RequesterType>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.RequesterType> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.RequesterType), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}