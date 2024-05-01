using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Services.Main
{
    internal class UploadRequest : EntityTypeConfiguration<Domain.Entities.Services.Main.UploadRequest>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Services.Main.UploadRequest> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Services.Main.UploadRequest), MappingDefaults.ServicesSchema);
            //builder.Property(e => e.IsApproved).HasDefaultValue(false);
            //builder.Property(e => e.IsActive).HasDefaultValue(false);
            base.Configure(builder);
        }
    }
}