using Framework.Core.Data.Repositories;

namespace QassimPrincipality.Domain.Interfaces
{
    public interface IRepositoryBaseStoreProcedure<TEntity> : IRepositoryBase<IAppDbContext, TEntity> where TEntity : class
    {
        public int ExecuteSP(string sql, params object[] parameters);

        public IQueryable<TEntity> ExecuteSPWithReturnData(string sql, params object[] parameters);
    }
}