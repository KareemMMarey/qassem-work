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
          Guid _EServiceAdmin = new Guid("AAAA4BA6-94D9-4395-BB1C-9D54939E70EF");
          Guid _OpenDataAdmin = new Guid("ED6D642F-50ED-4E28-BDE5-03B4C88F9387");
          Guid _ShareDataAdmin = new Guid("DB262BE8-1A1A-4910-99BA-65D89D8FD90E");
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
                    PhoneNumber = "+966564829845",
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

                var EServiceAdmin = new ApplicationUser("EServiceAdmin", "E-Service Admin")
                {
                    Id = _EServiceAdmin,
                    UserName = "EServiceAdmin",
                    NormalizedUserName = "ESERVICEADMIN",
                    FullName = "E-Service Admin",
                    NormalizedEmail = "ESERVICEADMIN@GMAIL.COM",
                    PhoneNumber = "+201000000000",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "EServiceAdmin@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                EServiceAdmin.PasswordHash = PassGenerate(EServiceAdmin);
                Data.Add(EServiceAdmin);

                var OpenDataAdmin = new ApplicationUser("OpenDataAdmin", "Open Data Admin")
                {
                    Id = _OpenDataAdmin,
                    UserName = "OpenDataAdmin",
                    NormalizedUserName = "OPENDATAADMIN",
                    FullName = "Open Data Admin",
                    NormalizedEmail = "OPENDATAADMIN@Gmail.com",
                    PhoneNumber = "+201000000111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "OpenDataAdmin@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                OpenDataAdmin.PasswordHash = PassGenerate(OpenDataAdmin);
                Data.Add(OpenDataAdmin);

                var ShareDataAdmin = new ApplicationUser("ShareDataAdmin", "Share Data Admin")
                {
                    Id = _ShareDataAdmin,
                    UserName = "ShareDataAdmin",
                    NormalizedUserName = "SHAREDATAADMIN",
                    FullName = "Share Data Admin",
                    NormalizedEmail = "SHAREDATAADMIN@Gmail.com",
                    PhoneNumber = "+201000000111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = new Guid().ToString("D"),
                    Email = "ShareDataAdmin@Gmail.com",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedOn = DateTime.Now
                };
                ShareDataAdmin.PasswordHash = PassGenerate(ShareDataAdmin);
                Data.Add(ShareDataAdmin);

                context.Set<ApplicationUser>().AddRange(Data); 
                context.SaveChanges();
            }
        }
        private static void SeedIdentityUsersRolesInfo(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>();
            Guid _adminId = new Guid("B22698B8-42A2-4115-9631-1C2D1E2AC5F7");
            Guid _EServiceAdmin = new Guid("AAAA4BA6-94D9-4395-BB1C-9D54939E70EF");
            Guid _OpenDataAdmin = new Guid("ED6D642F-50ED-4E28-BDE5-03B4C88F9387");
            Guid _ShareDataAdmin = new Guid("DB262BE8-1A1A-4910-99BA-65D89D8FD90E");
            context.Database.EnsureCreated();
            if (!context.Set<ApplicationUserRoles>().Any())
            {
                List<ApplicationUserRoles> Data = new List<ApplicationUserRoles>() {
                new ApplicationUserRoles
                {
                    Id = new Guid("533810E7-AD5A-4883-AC15-A004A48D51D5"),
                    RoleId = RoleHelper.Admin,
                    UserId = _adminId
                },
                 new ApplicationUserRoles
                {
                    Id = new Guid("EE143D53-814A-409D-B6E0-60B520750918"),
                    RoleId = RoleHelper.OpenDataRequestAdmin,
                    UserId = _OpenDataAdmin
                },
                  new ApplicationUserRoles
                {
                    Id = new Guid("1A9299BA-2253-4B7D-969F-6A8A7EE73A2B"),
                    RoleId = RoleHelper.ShareDataRequestAdmin,
                    UserId = _ShareDataAdmin
                },
                   new ApplicationUserRoles
                {
                    Id = new Guid("237601F2-CB12-42A6-8940-5E318F7186E3"),
                    RoleId = RoleHelper.EServicesRequestAdmin,
                    UserId = _EServiceAdmin
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