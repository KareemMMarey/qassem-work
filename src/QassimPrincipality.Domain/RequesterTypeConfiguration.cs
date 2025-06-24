using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace Framework.Identity.Data.Seed
{
    public class RequesterTypeConfiguration : IEntityTypeConfiguration<RequesterType>
    {
        public void Configure(EntityTypeBuilder<RequesterType> builder)
        {
            builder.HasData(
                new RequesterType
                {
                    Id = 1,
                    NameAr = "فرد",
                    NameEn = "Individual",
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                },
                new RequesterType
                {
                    Id = 2,
                    NameAr = "حكومة",
                    NameEn = "Government",
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                },
                new RequesterType
                {
                    Id = 3,
                    NameAr = "خاص",
                    NameEn = "Special",
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now,
                }
            );
        }
    }
}
