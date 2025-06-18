using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using QassimPrincipality.Application;
using QassimPrincipality.Infrastructure;
using Framework.Core.AutoMapper;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using QassimPrincipality.Web.Helpers;
using Framework.Core.Globalization;
using System.Configuration;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using Framework.Core.Notifications;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
namespace QassimPrincipality.Web
{
    public class Program
    {
		//dotnet ef migrations add AdjustAddRequest -c AppDbContext  -p src/QassimPrincipality.Infrastructure -s src/QassimPrincipality.Web
		//dotnet ef database update -c AppDbContext  -p src/QassimPrincipality.Infrastructure -s src/QassimPrincipality.Web
		//dotnet ef migrations add updateusertable -c AppIdentityDbContext  -p src/framework/Framework.Identity -s src/QassimPrincipality.Web
		public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

				builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

				builder.Services.Configure<RequestLocalizationOptions>(options =>
				{
					var supportedCultures = new[]
					{
				new CultureInfo("ar-SA"),
				new CultureInfo("en-US"),
			};

					options.DefaultRequestCulture = new RequestCulture("ar-SA");
					options.SupportedCultures = supportedCultures;
					options.SupportedUICultures = supportedCultures;
				});

				builder.Services.AddControllersWithViews().AddViewLocalization().AddDataAnnotationsLocalization(); ;

				// Add services to the container.
				//builder.Services.AddControllersWithViews();
				var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
                var mailConfiguration = builder.Configuration.GetSection("NafathConfiguration");
                builder.Services.Configure<NafathConfiguration>(mailConfiguration);
                var identityOptions = builder.Configuration.GetSection("IdentityConfigurations");
                builder.Services.Configure<IdentityConfigurations>(identityOptions);

            var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration");
            builder.Services.Configure<SmtpConfiguration>(emailConfiguration);

            var referralNumberConfiguration = builder.Configuration.GetSection("ReferralNumberConfiguration");
            builder.Services.Configure<ReferralNumberConfiguration>(referralNumberConfiguration);

            // Add services to the container.
            builder.Services.ConfigureSharedApplicationServices(connectionString);
            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureInfrastructureServices(connectionString);
            builder.Services.IdentityConfigureServices(connectionString);
            //builder.Services.ConfigureSharedApplicationServices(connectionString);
            //builder.Services.AddDataProtection(connectionString);
            //builder.Services.AddControllers().AddNewtonsoftJson();


                builder.Services.AddMemoryCache();
                builder.Services.AddSession();
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });
                builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    //options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the session expiry time

                    //options.LoginPath = "/Account/Login";
                    ////options.AccessDeniedPath = "/Account/AccessDenied";
                    //options.SlidingExpiration = true; // Refresh the cookie expiry on each request


                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    //options.SlidingExpiration = true;

                    // ✅ توجيه مخصص بناءً على نوع المستخدم والمسار
                    options.Events.OnRedirectToLogin = context =>
                    {
                        var path = context.Request.Path;
                        if (path.StartsWithSegments("/Admin"))
                        {
                            context.Response.Redirect("/Account/Login");
                        }
                        else
                        {
                            context.Response.Redirect("/Account/NafathLogin");
                        }
                        return Task.CompletedTask;
                    };
                });

                


                

                #region Nlog

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();
               

                #endregion Nlog

                var app = builder.Build();
                // Configure the middleware
                //app.UseMiddleware<RequestLoggingMiddleware>();
                app.UseMiddleware<ExceptionHandlingMiddleware>();
                // Configure the HTTP request pipeline.
                //if (!app.Environment.IsDevelopment())
                //{
                //    app.UseExceptionHandler("/Home/Error");
                //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts();
                //}
               

                if (app.Environment.IsDevelopment())
                {
                    //app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

				

				app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
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
               
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(typeof(AppAutoMapperProfile));
                });
                AutoMapperConfiguration.Init(config);

                app.UseApplicationDBMigration();
                app.UseSharedApplicationDBMigration();
                app.UseIdentityDBMigration();
                AppDbInitializer.Seed(builder.Services, builder);
                //app.UseMiddleware<GlobalizationMiddleware>();

                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
