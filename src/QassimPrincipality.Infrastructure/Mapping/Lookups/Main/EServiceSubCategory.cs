using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QassimPrincipality.Infrastructure.Mapping.Lookups.Main
{
    internal class EServiceSubCategory : EntityTypeConfiguration<Domain.Entities.Lookups.Main.EServiceSubCategory>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Lookups.Main.EServiceSubCategory> builder)
        {
            builder.ToTable(nameof(Domain.Entities.Lookups.Main.EServiceSubCategory), MappingDefaults.LookupSchema);
            builder.HasOne(e => e.EServiceCategory)
        .WithMany(e => e.EServiceSubCategories)
        .HasForeignKey(e => e.CategoryId)
        .HasPrincipalKey(e => e.Id);

            base.Configure(builder);
        }
    }
}