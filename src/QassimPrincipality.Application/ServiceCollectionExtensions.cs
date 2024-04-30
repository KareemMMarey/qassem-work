

using Framework.Core.BackgroundJobs;
using Framework.Core.DependencyManagement;
using Framework.Core.SharedServices.Services;
using Framework.Identity;
using Framework.Identity.Data.Services;
using Hangfire;
using Hangfire.Logging.LogProviders;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QassimPrincipality.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(BackgroundTasks)))
   .Where(c => c.Name.EndsWith("AppService"))
   .AsConcreteTypesScoped();

    //        services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(K2Proxy)))
    //.Where(c => c.Name.EndsWith("K2Proxy"))
    //.AsConcreteTypesScoped();

            services.AddTransient<AppSettingsService>();
            services.AddTransient<UserAppService>();
            services.AddTransient<UserRoleAppService>();
            services.AddTransient<RoleAppService>();
            services.AddTransient<UserTokensAppService>();
            services.AddTransient<IBackgroundTasks, BackgroundTasks>();
            services.AddTransient<ActiveDirectoryHelperAppService>();
        }

        public static void InitHangfire(this IServiceCollection services, string connectionString)
        {
            var sqlStorage = new SqlServerStorage(connectionString);

            JobStorage.Current = sqlStorage;

            services.AddHangfire(config =>
            {
                config.UseLogProvider(new ColouredConsoleLogProvider());

                GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString,
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
            });

            services.AddHangfireServer();

            var serviceProvider = services.BuildServiceProvider();
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));

            var backGroundTasks = serviceProvider.GetService<IBackgroundTasks>();
            backGroundTasks?.Init();
        }

        public static void UseHangfireDashboard(this IApplicationBuilder app)
        {
            var options = new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * 1 };

            app.UseHangfireServer(options);

            app.UseHangfireDashboard(
                "/back-jobs",
                new DashboardOptions { Authorization = new[] { new HangfireDashboardAuthFilter() } });
        }
    }
}