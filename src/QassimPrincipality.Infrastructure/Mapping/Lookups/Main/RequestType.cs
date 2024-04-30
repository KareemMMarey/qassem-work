using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class RequestType : EntityTypeConfiguration<Domain.Entities.Lookups.Main.RequestType>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.RequestType> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.RequestType), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}