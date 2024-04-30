using Framework.Core.Data.Repositories;

namespace QassimPrincipality.Domain.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryBase<IAppDbContext, TEntity> where TEntity : class
    {
    }
}