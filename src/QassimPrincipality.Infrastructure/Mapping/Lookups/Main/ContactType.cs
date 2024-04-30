using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class ContactType : EntityTypeConfiguration<Domain.Entities.Lookups.Main.ContactType>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.ContactType> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.ContactType), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}