using Framework.Core.DependencyManagement;
using Framework.Core.SharedServices.Entities;
using Framework.Core.SharedServices;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Repositories;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Framework.Identity.Data.Helper;

namespace Framework.Identity.Data
{
    public static class IdentityRegisterDependencies
    {
        public static void IdentityConfigureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<UserRolesRepository>();
            services.AddScoped<UserTokensRepository>();
            //services.AddScoped<UserAppService>();
            //services.AddScoped<RoleAppService>();
            services.ConfigureIdentityServices();

            //services.AddScoped<NotificationService>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "AspCoreIdentity";
            //    options.ExpireTimeSpan = TimeSpan.FromHours(24);
            //    options.LoginPath = "/ADIdentity/Account/Login";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 0;

                // Default SignIn settings.
                //options.SignIn.RequireConfirmedEmail = true;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void AddDataProtection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataKeysContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDataProtection()
                .SetApplicationName("DSC-WebApp")
                .PersistKeysToDbContext<DataKeysContext>();

            services.Configure<DataProtectionTokenProviderOptions>(
                options =>
                    options.TokenLifespan = TimeSpan.FromHours(24)
            );
        }

        public static void UseIdentityDBMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppIdentityDbContext>().Database.Migrate();
                SeedIdentityRolesInfo(serviceScope);
                SeedIdentityUsersInfo(serviceScope);
                SeedIdentityUsersRolesInfo(serviceScope);
            }
        }

        public static void UseDataKeysMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DataKeysContext>().Database.Migrate();
            }
        }

        public static void ConfigureIdentityServices(this IServiceCollection services)
        {
            //Auto Register App services As Scoped
            //services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(UserAppService)))
            //    .Where(c => c.Name.EndsWith("AppService"))
            //    .AsConcreteTypesScoped();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(IUserAppService)))
                .Where(c => c.Name.EndsWith("AppService"))
                .AsPublicImplementedInterfaces();
        }
        private static void SeedIdentityRolesInfo(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>();

            context.Database.EnsureCreated();
            if (!context.Set<ApplicationRole>().Any())
            {
                context.Set<ApplicationRole>().AddRange(new List<ApplicationRole>()
                    {
                        new ApplicationRole
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

                    }); ;
                context.SaveChanges();
            }
        }
        private static void SeedIdentityUsersInfo(IServiceScope serviceScope)
        {
          Guid _adminId = new Guid("B22698B8-42A2-4115-9631-1C2D1E2AC5F7");
          Guid _individualId = new Guid("AAAA4BA6-94D9-4395-BB1C-9D54939E70EF");
          Guid _companyId = new Guid("ED6D642F-50ED-4E28-BDE5-03B4C88F9387");
          Guid _AuctionSupervisorId = new Guid("DB262BE8-1A1A-4910-99BA-65D89D8FD90E");
        var context = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>();

            context.Database.EnsureCreated();
            if (!context.Set<ApplicationUser>().Any())
            {
                List<ApplicationUser> Data = new List<ApplicationUser>();
                var admin = new ApplicationUser("admin", "Administrator")
                {
                    Id = _adminId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FullName = "Administrator",
                    NormalizedEmail = "KAREEM.M.MAREY@GMAIL.COM",
                    PhoneNumber = "+201012222212",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "kareem.m.marey@gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                admin.PasswordHash = PassGenerate(admin);
                Data.Add(admin);

                var individual = new ApplicationUser("admin", "Administrator")
                {
                    Id = _individualId,
                    UserName = "individual",
                    NormalizedUserName = "INDIVIDUAL",
                    FullName = "Individual",
                    NormalizedEmail = "Individual@Gmail.com",
                    PhoneNumber = "+201000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "Individual@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                individual.PasswordHash = PassGenerate(individual);
                Data.Add(individual);

                var company = new ApplicationUser("admin", "Administrator")
                {
                    Id = _companyId,
                    UserName = "company",
                    NormalizedUserName = "COMPANY",
                    FullName = "Company",
                    NormalizedEmail = "Company@Gmail.com",
                    PhoneNumber = "+201000000111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "Company@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                company.PasswordHash = PassGenerate(company);
                Data.Add(company);

                var AuctionSupervisor = new ApplicationUser("admin", "Administrator")
                {
                    Id = _AuctionSupervisorId,
                    UserName = "AuctionSupervisor",
                    NormalizedUserName = "AuctionSupervisor",
                    FullName = "AuctionSupervisor",
                    NormalizedEmail = "auctionSupervisor@Gmail.com",
                    PhoneNumber = "+201000001111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "auctionSupervisor@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                AuctionSupervisor.PasswordHash = PassGenerate(AuctionSupervisor);
                Data.Add(AuctionSupervisor);

                context.Set<ApplicationUser>().AddRange(Data); 
                context.SaveChanges();
            }
        }
        private static void SeedIdentityUsersRolesInfo(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>();
              Guid _adminId = new Guid("B22698B8-42A2-4115-9631-1C2D1E2AC5F7");
        context.Database.EnsureCreated();
            if (!context.Set<ApplicationUserRoles>().Any())
            {
                List<ApplicationUserRoles> Data = new List<ApplicationUserRoles>() {
                new ApplicationUserRoles
                {
                    Id = new Guid("533810E7-AD5A-4883-AC15-A004A48D51D5"),
                    RoleId = RoleHelper.Admin,
                    UserId = _adminId
                }
                };

                context.Set<ApplicationUserRoles>().AddRange(Data);
                context.SaveChanges();
            }
        }
        public static string PassGenerate(ApplicationUser user)
        {
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, "P@ssw0rd");

        }
    }
}