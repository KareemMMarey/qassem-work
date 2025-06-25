using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Configurations
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
                    CreatedOn = new DateTime(
                        2025,
                        6,
                        25,
                        22,
                        13,
                        27,
                        230,
                        DateTimeKind.Local
                    ).AddTicks(141),
                },
                new RequesterType
                {
                    Id = 2,
                    NameAr = "حكومة",
                    NameEn = "Government",
                    CreatedBy = "Admin",
                    CreatedOn = new DateTime(
                        2025,
                        6,
                        25,
                        22,
                        13,
                        27,
                        230,
                        DateTimeKind.Local
                    ).AddTicks(141),
                },
                new RequesterType
                {
                    Id = 3,
                    NameAr = "خاص",
                    NameEn = "Special",
                    CreatedBy = "Admin",
                    CreatedOn = new DateTime(
                        2025,
                        6,
                        25,
                        22,
                        13,
                        27,
                        230,
                        DateTimeKind.Local
                    ).AddTicks(141),
                }
            );
        }
    }
}
