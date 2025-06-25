using Framework.Core.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QassimPrincipality.Domain.Entities.Lookups.Main;

namespace QassimPrincipality.Domain.Configurations
{
    public class EntityTypeConfiguration : EntityTypeConfiguration<EntityType>
    {
        public override void Configure(EntityTypeBuilder<EntityType> builder)
        {
            builder.HasData(
                new EntityType
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
                new EntityType
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
                new EntityType
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
