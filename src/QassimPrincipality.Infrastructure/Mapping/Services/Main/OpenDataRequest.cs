using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Services.Main
{
    internal class OpenDataRequest : EntityTypeConfiguration<Domain.Entities.Services.Main.OpenDataRequest>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Services.Main.OpenDataRequest> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Services.Main.OpenDataRequest), MappingDefaults.ServicesSchema);
            base.Configure(builder);
        }
    }
}