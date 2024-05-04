using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Services.Main
{
    internal class ShareDataRequest : EntityTypeConfiguration<Domain.Entities.Services.Main.ShareDataRequest>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Services.Main.ShareDataRequest> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Services.Main.ShareDataRequest), MappingDefaults.ServicesSchema);
            base.Configure(builder);
        }
    }
}