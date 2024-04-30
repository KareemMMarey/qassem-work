using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Services.Main
{
    internal class ContactForm : EntityTypeConfiguration<Domain.Entities.Services.Main.ContactForm>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Services.Main.ContactForm> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Services.Main.ContactForm), MappingDefaults.ServicesSchema);
            base.Configure(builder);
        }
    }
}