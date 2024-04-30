using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Data.Repositories;

namespace QassimPrincipality.Infrastructure.Data
{
    public class Repository<TEntity> : RepositoryBase<IAppDbContext, TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(IAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}