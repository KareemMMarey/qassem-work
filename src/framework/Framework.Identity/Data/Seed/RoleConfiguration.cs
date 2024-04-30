//using Framework.Identity.Data.Entities;
//using Framework.Identity.Data.Helper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;

//namespace Framework.Identity.Data.Seed
//{
//    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
//    {
//        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
//        {
//            builder.HasData(new ApplicationRole
//            {
//                Id = RoleHelper.Admin,
//                Name = "Admin",
//                Code = 1,
//                NormalizedName = "ADMIN",
//                DisplayNameAr = "مدير النظام",
//                DisplayNameEn = "Administrator",
//                DescriptionAr = "مدير النظام",
//                DescriptionEn = "Administrator",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }, new ApplicationRole
//            {
//                Id = RoleHelper.Individual,
//                Name = "Individual",
//                Code = 2,
//                NormalizedName = "INDIVIDUAL",
//                DisplayNameAr = "فرد",
//                DisplayNameEn = "Individual",
//                DescriptionAr = "فرد",
//                DescriptionEn = "Individual",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.ExternalIndividual,
//                Name = "ExternalIndividual",
//                Code = 16,
//                NormalizedName = "EXTERNALINDIVIDUAL",
//                DisplayNameAr = "فرد خارجي",
//                DisplayNameEn = "ExternalIndividual",
//                DescriptionAr = "فرد خارجي",
//                DescriptionEn = "ExternalIndividual",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }, new ApplicationRole
//            {
//                Id = RoleHelper.Company,
//                Name = "Company",
//                Code = 3,
//                NormalizedName = "COMPANY",
//                DisplayNameAr = "شركه",
//                DisplayNameEn = "Company",
//                DescriptionAr = "شركه",
//                DescriptionEn = "Company",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now
//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.AuctionSupervisor,
//                Name = "AuctionSupervisor",
//                Code = 4,
//                NormalizedName = "AuctionSupervisor",
//                DisplayNameAr = "مشرف المزاد",
//                DisplayNameEn = "AuctionSupervisor",
//                DescriptionAr = "مشرف المزاد",
//                DescriptionEn = "AuctionSupervisor",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.CentralOperationsOfficer,
//                Name = "CentralOperationsOfficer",
//                Code = 5,
//                NormalizedName = "CentralOperationsOfficer",
//                DisplayNameAr = "موظف العمليات المركزية",
//                DisplayNameEn = "CentralOperationsOfficer",
//                DescriptionAr = "موظف العمليات المركزية",
//                DescriptionEn = "CentralOperationsOfficer",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.SectorHead,
//                Name = "SectorHead",
//                Code = 6,
//                NormalizedName = "SectorHead",
//                DisplayNameAr = "رئيس القطاع",
//                DisplayNameEn = "SectorHead",
//                DescriptionAr = "رئيس القطاع",
//                DescriptionEn = "SectorHead",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.CentralOperationsGM,
//                Name = "CentralOperationsGM",
//                Code = 7,
//                NormalizedName = "CentralOperationsGM",
//                DisplayNameAr = "مدير عام العمليات المركزية",
//                DisplayNameEn = "CentralOperationsGM",
//                DescriptionAr = "مدير عام العمليات المركزية",
//                DescriptionEn = "Central Operations GM",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.DebtDepartmentManager,
//                Name = "DebtDepartmentManager",
//                Code = 8,
//                NormalizedName = "DebtDepartmentManager",
//                DisplayNameAr = "مدير إدارة الدين",
//                DisplayNameEn = "DebtDepartmentManager",
//                DescriptionAr = "مدير إدارة الدين",
//                DescriptionEn = "Debt Department Manager",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.CommitmentOfficer,
//                Name = "CommitmentOfficer",
//                Code = 9,
//                NormalizedName = "CommitmentOfficer",
//                DisplayNameAr = "مدير الالتزام",
//                DisplayNameEn = "Commitment Officer",
//                DescriptionAr = "مدير الالتزام",
//                DescriptionEn = "Commitment Officer",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.PortSupervisor,
//                Name = "PortSupervisor",
//                Code = 10,
//                NormalizedName = "PortSupervisor",
//                DisplayNameAr = "مشرف الجمرك",
//                DisplayNameEn = "Port Supervisor",
//                DescriptionAr = "مشرف الجمرك",
//                DescriptionEn = "Port Supervisor",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.AuctionManager,
//                Name = "AuctionManager",
//                Code = 11,
//                NormalizedName = "AuctionManager",
//                DisplayNameAr = "مدير المزادات",
//                DisplayNameEn = "Auction Manager",
//                DescriptionAr = "مدير المزادات",
//                DescriptionEn = "Auction Manager",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.DestructionSupervisor,
//                Name = "DestructionSupervisor",
//                Code = 12,
//                NormalizedName = "DestructionSupervisor",
//                DisplayNameAr = "مشرف الاتلاف",
//                DisplayNameEn = "Destruction Supervisor",
//                DescriptionAr = "مشرف الاتلاف",
//                DescriptionEn = "Destruction Supervisor",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.PortTechnicalSupportOfficer,
//                Name = "PortTechnicalSupportOfficer",
//                Code = 13,
//                NormalizedName = "PortTechnicalSupportOfficer",
//                DisplayNameAr = "موظف الدعم الفني بالجمرك",
//                DisplayNameEn = "Port Technical Support Officer",
//                DescriptionAr = "موظف الدعم الفني بالجمرك",
//                DescriptionEn = "Port Technical Support Officer",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            , new ApplicationRole
//            {
//                Id = RoleHelper.CorporateCommunicationMember,
//                Name = "CorporateCommunicationMember",
//                Code = 14,
//                NormalizedName = "CorporateCommunicationMember",
//                DisplayNameAr = "عضو الاتصال المؤسسي",
//                DisplayNameEn = "Corporate Communication Member",
//                DescriptionAr = "عضو الاتصال المؤسسي",
//                DescriptionEn = "Corporate Communication Member",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }, new ApplicationRole
//            {
//                Id = RoleHelper.CommitteeUser,
//                Name = "CommitteeUser",
//                Code = 15,
//                NormalizedName = "CommitteeUser",
//                DisplayNameAr = "عضو لجنة جرد",
//                DisplayNameEn = "Committee User",
//                DescriptionAr = "عضو لجنة جرد",
//                DescriptionEn = "Committee User",
//                CreatedBy = "Admin",
//                CreatedOn = DateTime.Now

//            }
//            );
//        }
//    }
//}