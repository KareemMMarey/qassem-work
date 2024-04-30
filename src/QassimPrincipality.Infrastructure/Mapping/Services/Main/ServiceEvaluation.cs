using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Services.Main
{
    internal class ServiceEvaluation : EntityTypeConfiguration<Domain.Entities.Services.Main.ServiceEvaluation>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Services.Main.ServiceEvaluation> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Services.Main.ServiceEvaluation), MappingDefaults.ServicesSchema);
            base.Configure(builder);
        }
    }
}