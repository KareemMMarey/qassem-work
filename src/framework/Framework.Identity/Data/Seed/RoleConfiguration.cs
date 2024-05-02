using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Framework.Identity.Data.Seed
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole
            {
                Id = RoleHelper.Admin,
                Name = "Admin",
                NormalizedName = "ADMIN",
                DisplayNameAr = "مدير النظام",
                DisplayNameEn = "Administrator",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now

            }, new ApplicationRole
            {
                Id = RoleHelper.Individual,
                Name = "Individual",
                NormalizedName = "INDIVIDUAL",
                DisplayNameAr = "فرد",
                DisplayNameEn = "Individual",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now

            }
            , new ApplicationRole
            {
                Id = RoleHelper.OpenDataRequestAdmin,
                Name = "OpenDataRequestAdmin",
                NormalizedName = "OPENDATAREQUESTADMIN",
                DisplayNameAr = "إدارة البيانات المفتوحة",
                DisplayNameEn = "OpenDataRequestAdmin",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now

            }, new ApplicationRole
            {
                Id = RoleHelper.ShareDataRequestAdmin,
                Name = "ShareDataRequestAdmin",
                NormalizedName = "SHAREDATAREQUESTADMIN",
                DisplayNameAr = "إدارة مشاركة البيانات",
                DisplayNameEn = "ShareDataRequestAdmin",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now
            },
            new ApplicationRole
            {
                Id = RoleHelper.EServicesRequestAdmin,
                Name = "EServicesRequestAdmin",
                NormalizedName = "ESRVICESREQUESTADMIN",
                DisplayNameAr = "إدارة طلبات الخدمات الإلكترونية",
                DisplayNameEn = "EServicesRequestAdmin",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now
            }
            );
        }
    }
}