using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Data;
using Framework.Core.Data.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using QassimPrincipality.Domain.Entities.Services.Main;
using Framework.Identity.Data.Seed;

namespace QassimPrincipality.Infrastructure.Data
{
    public class AppDbContext : BaseDbContext<AppDbContext>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
            //CurrentUserName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            CurrentUserName = ((System.Security.Claims.ClaimsIdentity)httpContextAccessor?.HttpContext?.User?.Identity)?.
    FindFirst("fullName")?.Value;
        }
        public virtual DbSet<OpenDataRequest> OpenDataRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            modelBuilder.ApplyConfiguration(new RequesterTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}