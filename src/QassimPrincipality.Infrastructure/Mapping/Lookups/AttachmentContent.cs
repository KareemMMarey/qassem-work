using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups
{
    internal class AttachmentContent : EntityTypeConfiguration<Domain.Entities.Lookups.AttachmentContent>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.AttachmentContent> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.AttachmentContent), MappingDefaults.LookupSchema);

            base.Configure(builder);
        }
    }
}