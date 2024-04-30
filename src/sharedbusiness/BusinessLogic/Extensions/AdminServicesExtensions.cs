


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Resources;

namespace BusinessLogic.Extensions
{
    public static class AdminServicesExtensions
    {
        public static IServiceCollection AddAdminServices<TAdminDbContext>(
            this IServiceCollection services)
            where TAdminDbContext : DbContext
        {

            return services.AddAdminServices();
        }

        public static IServiceCollection AddAdminServices(this IServiceCollection services)
        {
            //Repositories
            //services.AddTransient<IClientRepository, ClientRepository<TConfigurationDbContext>>();
            //services.AddTransient<IIdentityResourceRepository, IdentityResourceRepository<TConfigurationDbContext>>();
            //services.AddTransient<IApiResourceRepository, ApiResourceRepository<TConfigurationDbContext>>();
            //services.AddTransient<IApiScopeRepository, ApiScopeRepository<TConfigurationDbContext>>();
            //services.AddTransient<IPersistedGrantRepository, PersistedGrantRepository<TPersistedGrantDbContext>>();
            //services.AddTransient<IIdentityProviderRepository, IdentityProviderRepository<TConfigurationDbContext>>();
            //services.AddTransient<IKeyRepository, KeyRepository<TPersistedGrantDbContext>>();
            //services.AddTransient<ILogRepository, LogRepository<TLogDbContext>>();

            //Services
            //services.AddTransient<IClientService, ClientService>();
            //services.AddTransient<IApiResourceService, ApiResourceService>();
            //services.AddTransient<IApiScopeService, ApiScopeService>();
            //services.AddTransient<IIdentityResourceService, IdentityResourceService>();
            //services.AddTransient<IIdentityProviderService, IdentityProviderService>();
            //services.AddTransient<IPersistedGrantService, PersistedGrantService>();
            //services.AddTransient<IKeyService, KeyService>();
            //services.AddTransient<ILogService, LogService>();

            //Resources
            services.AddScoped<IApiResourceServiceResources, ApiResourceServiceResources>();
            services.AddScoped<IApiScopeServiceResources, ApiScopeServiceResources>();
            services.AddScoped<IClientServiceResources, ClientServiceResources>();
            services.AddScoped<IIdentityResourceServiceResources, IdentityResourceServiceResources>();
            services.AddScoped<IIdentityProviderServiceResources, IdentityProviderServiceResources>();
            services.AddScoped<IPersistedGrantServiceResources, PersistedGrantServiceResources>();
            services.AddScoped<IKeyServiceResources, KeyServiceResources>();

            return services;
        }
    }
}
