using QassimPrincipality.Domain.Entities.Lookups.Main;
using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Data;
using Framework.Core.Data.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

           

            base.OnModelCreating(modelBuilder);
        }
    }
}