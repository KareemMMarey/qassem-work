using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups
{
    internal class Attachment : EntityTypeConfiguration<Domain.Entities.Lookups.Attachment>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Attachment> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Attachment), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}