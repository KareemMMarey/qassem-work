using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using QassimPrincipality.Application;
using QassimPrincipality.Infrastructure;
using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace QassimPrincipality.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

            // Add services to the container.
            builder.Services.ConfigureSharedApplicationServices(connectionString);
            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureInfrastructureServices(connectionString);
            builder.Services.IdentityConfigureServices(connectionString);
            builder.Services.AddDataProtection(connectionString);
            //builder.Services.AddControllers().AddNewtonsoftJson();


            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            builder.Services.AddControllersWithViews();
            #region Nlog

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            #endregion Nlog

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.UseCors();
            //app.UseHttpsRedirection();
            //app.MapControllers();
            // create AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(AppAutoMapperProfile));
            });
            AutoMapperConfiguration.Init(config);

            app.UseApplicationDBMigration();
            app.UseSharedApplicationDBMigration();
            app.UseIdentityDBMigration();

            app.Run();
        }
    }
}
