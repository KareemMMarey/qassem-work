
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using QassimPrincipality.Web.Identity.Configuration;
using QassimPrincipality.Web.Identity.Configuration.ApplicationParts;
using QassimPrincipality.Web.Identity.Configuration.Constants;
using QassimPrincipality.Web.Identity.Configuration.Interfaces;
using QassimPrincipality.Web.Identity.Helpers.Localization;

namespace QassimPrincipality.Web.Identity.Helpers
{
    public static class StartupHelpers
    {
        /// <summary>
        /// Register services for MVC and localization including available languages
        /// </summary>
        /// <param name="services"></param>
        public static IMvcBuilder AddMvcWithLocalization<TUser, TKey>(this IServiceCollection services, IConfiguration configuration)
            where TUser : IdentityUser<TKey>
            where TKey : IEquatable<TKey>
        {
            services.AddLocalization(opts => { opts.ResourcesPath = ConfigurationConsts.ResourcesPath; });

            services.TryAddTransient(typeof(IGenericControllerLocalizer<>), typeof(GenericControllerLocalizer<>));

            var mvcBuilder = services.AddControllersWithViews(o =>
                {
                    o.Conventions.Add(new GenericControllerRouteConvention());
                })
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = ConfigurationConsts.ResourcesPath; })
                .AddDataAnnotationsLocalization()
                .ConfigureApplicationPartManager(m =>
                {
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider<TUser, TKey>());
                });

            var cultureConfiguration = configuration.GetSection(nameof(CultureConfiguration)).Get<CultureConfiguration>();
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    // If cultures are specified in the configuration, use them (making sure they are among the available cultures),
                    // otherwise use all the available cultures
                    var supportedCultureCodes = (cultureConfiguration?.Cultures?.Count > 0 ?
                        cultureConfiguration.Cultures.Intersect(CultureConfiguration.AvailableCultures) :
                        CultureConfiguration.AvailableCultures).ToArray();

                    if (!supportedCultureCodes.Any()) supportedCultureCodes = CultureConfiguration.AvailableCultures;
                    var supportedCultures = supportedCultureCodes.Select(c => new CultureInfo(c)).ToList();

                    // If the default culture is specified use it, otherwise use CultureConfiguration.DefaultRequestCulture ("en")
                    var defaultCultureCode = string.IsNullOrEmpty(cultureConfiguration?.DefaultCulture) ?
                        CultureConfiguration.DefaultRequestCulture : cultureConfiguration?.DefaultCulture;

                    // If the default culture is not among the supported cultures, use the first supported culture as default
                    if (!supportedCultureCodes.Contains(defaultCultureCode)) defaultCultureCode = supportedCultureCodes.FirstOrDefault();

                    opts.DefaultRequestCulture = new RequestCulture(defaultCultureCode);
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;
                });

            return mvcBuilder;
        }

        /// <summary>
        /// Using of Forwarded Headers and Referrer Policy
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseSecurityHeaders(this IApplicationBuilder app, IConfiguration configuration)
        {
            var forwardingOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.All
            };

            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardingOptions);

            app.UseReferrerPolicy(options => options.NoReferrer());

            // CSP Configuration to be able to use external resources
            var cspTrustedDomains = new List<string>();
            configuration.GetSection(ConfigurationConsts.CspTrustedDomainsKey).Bind(cspTrustedDomains);
            if (cspTrustedDomains.Any())
            {
                app.UseCsp(csp =>
                {
                    var imagesSources = new List<string> { "data:" };
                    imagesSources.AddRange(cspTrustedDomains);

                    csp.ImageSources(options =>
                    {
                        options.SelfSrc = true;
                        options.CustomSources = imagesSources;
                        options.Enabled = true;
                    });
                    csp.FontSources(options =>
                    {
                        options.SelfSrc = true;
                        options.CustomSources = cspTrustedDomains;
                        options.Enabled = true;
                    });
                    csp.ScriptSources(options =>
                    {
                        options.SelfSrc = true;
                        options.CustomSources = cspTrustedDomains;
                        options.Enabled = true;
                        options.UnsafeInlineSrc = true;
                    });
                    csp.StyleSources(options =>
                    {
                        options.SelfSrc = true;
                        options.CustomSources = cspTrustedDomains;
                        options.Enabled = true;
                        options.UnsafeInlineSrc = true;
                    });
                    csp.Sandbox(options =>
                    {
                        options.AllowForms()
                            .AllowSameOrigin()
                            .AllowScripts();
                    });
                    csp.FrameAncestors(option =>
                    {
                        option.NoneSrc = true;
                        option.Enabled = true;
                    });

                    csp.BaseUris(options =>
                    {
                        options.SelfSrc = true;
                        options.Enabled = true;
                    });

                    csp.ObjectSources(options =>
                    {
                        options.NoneSrc = true;
                        options.Enabled = true;
                    });

                    csp.DefaultSources(options =>
                    {
                        options.Enabled = true;
                        options.SelfSrc = true;
                        options.CustomSources = cspTrustedDomains;
                    });
                });
            }

        }

        

        /// <summary>
        /// Add services for authentication, including Identity model, Duende IdentityServer and external providers
        /// </summary>
        /// <typeparam name="TIdentityDbContext">DbContext for Identity</typeparam>
        /// <typeparam name="TUserIdentity">User Identity class</typeparam>
        /// <typeparam name="TUserIdentityRole">User Identity Role class</typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuthenticationServices<TUserIdentity, TUserIdentityRole>(this IServiceCollection services, IConfiguration configuration) 
            where TUserIdentity : class
            where TUserIdentityRole : class
        {
            var loginConfiguration = GetLoginConfiguration(configuration);
            var registrationConfiguration = GetRegistrationConfiguration(configuration);
            var identityOptions = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();

            services
                .AddSingleton(registrationConfiguration)
                .AddSingleton(loginConfiguration)
                .AddSingleton(identityOptions);
                //.AddScoped<ApplicationSignInManager<TUserIdentity>>()
                //.AddScoped<UserResolver<TUserIdentity>>()
                //.AddIdentity<TUserIdentity, TUserIdentityRole>(options => configuration.GetSection(nameof(IdentityOptions)).Bind(options))
                //.AddEntityFrameworkStores<TIdentityDbContext>()
                //.AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.Secure = CookieSecurePolicy.SameAsRequest;
                //options.OnAppendCookie = cookieContext =>
                //    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                //options.OnDeleteCookie = cookieContext =>
                //    AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var authenticationBuilder = services.AddAuthentication();

            //AddExternalProviders(authenticationBuilder, configuration);
        }

        /// <summary>
        /// Get configuration for login
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static LoginConfiguration GetLoginConfiguration(IConfiguration configuration)
        {
            var loginConfiguration = configuration.GetSection(nameof(LoginConfiguration)).Get<LoginConfiguration>();

            // Cannot load configuration - use default configuration values
            if (loginConfiguration == null)
            {
                return new LoginConfiguration();
            }

            return loginConfiguration;
        }

        /// <summary>
        /// Get configuration for registration
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static RegisterConfiguration GetRegistrationConfiguration(IConfiguration configuration)
        {
            var registerConfiguration = configuration.GetSection(nameof(RegisterConfiguration)).Get<RegisterConfiguration>();

            // Cannot load configuration - use default configuration values
            if (registerConfiguration == null)
            {
                return new RegisterConfiguration();
            }

            return registerConfiguration;
        }

        ///// <summary>
        ///// Add configuration for Duende IdentityServer
        ///// </summary>
        ///// <typeparam name="TUserIdentity"></typeparam>
        ///// <typeparam name="TConfigurationDbContext"></typeparam>
        ///// <typeparam name="TPersistedGrantDbContext"></typeparam>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        //public static IIdentityServerBuilder AddIdentityServer<TConfigurationDbContext, TPersistedGrantDbContext, TUserIdentity>(
        //    this IServiceCollection services,
        //    IConfiguration configuration)
        //    where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
        //    where TConfigurationDbContext : DbContext, IAdminConfigurationDbContext
        //    where TUserIdentity : class
        //{
        //    var configurationSection = configuration.GetSection(nameof(IdentityServerOptions));

        //    var identityServerOptions = configurationSection.Get<IdentityServerOptions>();

        //    var builder = services.AddIdentityServer(options =>
        //        {
        //            configurationSection.Bind(options);

        //            options.DynamicProviders.SignInScheme = IdentityConstants.ExternalScheme;
        //            options.DynamicProviders.SignOutScheme = IdentityConstants.ApplicationScheme;
        //        })
        //        .AddConfigurationStore<TConfigurationDbContext>()
        //        .AddOperationalStore<TPersistedGrantDbContext>()
        //        .AddAspNetIdentity<TUserIdentity>();

        //    //services.ConfigureOptions<OpenIdClaimsMappingConfig>();

        //    if (!identityServerOptions.KeyManagement.Enabled)
        //    {
        //        builder.AddCustomSigningCredential(configuration);
        //        builder.AddCustomValidationKey(configuration);
        //    }

        //    //builder.AddExtensionGrantValidator<DelegationGrantValidator>();

        //    return builder;
        //}

        ///// <summary>
        ///// Add external providers
        ///// </summary>
        ///// <param name="authenticationBuilder"></param>
        ///// <param name="configuration"></param>
        //private static void AddExternalProviders(AuthenticationBuilder authenticationBuilder,
        //    IConfiguration configuration)
        //{
        //    var externalProviderConfiguration = configuration.GetSection(nameof(ExternalProvidersConfiguration)).Get<ExternalProvidersConfiguration>();

        //    if (externalProviderConfiguration.UseGitHubProvider)
        //    {
        //        authenticationBuilder.AddGitHub(options =>
        //        {
        //            options.ClientId = externalProviderConfiguration.GitHubClientId;
        //            options.ClientSecret = externalProviderConfiguration.GitHubClientSecret;
        //            options.CallbackPath = externalProviderConfiguration.GitHubCallbackPath;
        //            options.Scope.Add("user:email");
        //        });
        //    }

        //    if (externalProviderConfiguration.UseAzureAdProvider)
        //    {
        //        authenticationBuilder.AddMicrosoftIdentityWebApp(options =>
        //          {
        //              options.ClientSecret = externalProviderConfiguration.AzureAdSecret;
        //              options.ClientId = externalProviderConfiguration.AzureAdClientId;
        //              options.TenantId = externalProviderConfiguration.AzureAdTenantId;
        //              options.Instance = externalProviderConfiguration.AzureInstance;
        //              options.Domain = externalProviderConfiguration.AzureDomain;
        //              options.CallbackPath = externalProviderConfiguration.AzureAdCallbackPath;
        //          }, cookieScheme: null);
        //    }
        //}

        /// <summary>
        /// Register middleware for localization
        /// </summary>
        /// <param name="app"></param>
        public static void UseMvcLocalizationServices(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }

        /// <summary>
        /// Add authorization policies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootConfiguration"></param>
        public static void AddAuthorizationPolicies(this IServiceCollection services,
                IRootConfiguration rootConfiguration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy => policy.RequireRole(rootConfiguration.AdminConfiguration.AdministrationRole));
            });
        }

        //public static void AddIdSHealthChecks<TConfigurationDbContext, TPersistedGrantDbContext, TIdentityDbContext, TDataProtectionDbContext>(this IServiceCollection services, IConfiguration configuration)
        //    where TConfigurationDbContext : DbContext, IAdminConfigurationDbContext
        //    where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
        //    where TIdentityDbContext : DbContext
        //    where TDataProtectionDbContext : DbContext, IDataProtectionKeyContext
        //{
        //    var configurationDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.ConfigurationDbConnectionStringKey);
        //    var persistedGrantsDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.PersistedGrantDbConnectionStringKey);
        //    var identityDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.IdentityDbConnectionStringKey);
        //    var dataProtectionDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.DataProtectionDbConnectionStringKey);

        //    var healthChecksBuilder = services.AddHealthChecks()
        //        .AddDbContextCheck<TConfigurationDbContext>("ConfigurationDbContext")
        //        .AddDbContextCheck<TPersistedGrantDbContext>("PersistedGrantsDbContext")
        //        .AddDbContextCheck<TIdentityDbContext>("IdentityDbContext")
        //        .AddDbContextCheck<TDataProtectionDbContext>("DataProtectionDbContext");

        //    var serviceProvider = services.BuildServiceProvider();
        //    var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //        var configurationTableName = DbContextHelpers.GetEntityTable<TConfigurationDbContext>(scope.ServiceProvider);
        //        var persistedGrantTableName = DbContextHelpers.GetEntityTable<TPersistedGrantDbContext>(scope.ServiceProvider);
        //        var identityTableName = DbContextHelpers.GetEntityTable<TIdentityDbContext>(scope.ServiceProvider);
        //        var dataProtectionTableName = DbContextHelpers.GetEntityTable<TDataProtectionDbContext>(scope.ServiceProvider);

        //        var databaseProvider = configuration.GetSection(nameof(DatabaseProviderConfiguration)).Get<DatabaseProviderConfiguration>();
        //        switch (databaseProvider.ProviderType)
        //        {
        //            case DatabaseProviderType.SqlServer:
        //                healthChecksBuilder
        //                    .AddSqlServer(configurationDbConnectionString, name: "ConfigurationDb",
        //                        healthQuery: $"SELECT TOP 1 * FROM dbo.[{configurationTableName}]")
        //                    .AddSqlServer(persistedGrantsDbConnectionString, name: "PersistentGrantsDb",
        //                        healthQuery: $"SELECT TOP 1 * FROM dbo.[{persistedGrantTableName}]")
        //                    .AddSqlServer(identityDbConnectionString, name: "IdentityDb",
        //                        healthQuery: $"SELECT TOP 1 * FROM dbo.[{identityTableName}]")
        //                    .AddSqlServer(dataProtectionDbConnectionString, name: "DataProtectionDb",
        //                        healthQuery: $"SELECT TOP 1 * FROM dbo.[{dataProtectionTableName}]");

        //                break;
        //            case DatabaseProviderType.PostgreSQL:
        //                healthChecksBuilder
        //                    .AddNpgSql(configurationDbConnectionString, name: "ConfigurationDb",
        //                        healthQuery: $"SELECT * FROM \"{configurationTableName}\" LIMIT 1")
        //                    .AddNpgSql(persistedGrantsDbConnectionString, name: "PersistentGrantsDb",
        //                        healthQuery: $"SELECT * FROM \"{persistedGrantTableName}\" LIMIT 1")
        //                    .AddNpgSql(identityDbConnectionString, name: "IdentityDb",
        //                        healthQuery: $"SELECT * FROM \"{identityTableName}\" LIMIT 1")
        //                    .AddNpgSql(dataProtectionDbConnectionString, name: "DataProtectionDb",
        //                        healthQuery: $"SELECT * FROM \"{dataProtectionTableName}\"  LIMIT 1");
        //                break;
        //            case DatabaseProviderType.MySql:
        //                healthChecksBuilder
        //                    .AddMySql(configurationDbConnectionString, name: "ConfigurationDb")
        //                    .AddMySql(persistedGrantsDbConnectionString, name: "PersistentGrantsDb")
        //                    .AddMySql(identityDbConnectionString, name: "IdentityDb")
        //                    .AddMySql(dataProtectionDbConnectionString, name: "DataProtectionDb");
        //                break;
        //            default:
        //                throw new NotImplementedException($"Health checks not defined for database provider {databaseProvider.ProviderType}");
        //        }
        //    }
        //}
    }
}
