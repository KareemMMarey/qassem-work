using Framework.Core.Data.Uow;

namespace QassimPrincipality.Domain.Interfaces
{
    public interface IUnitOfWork : IUnitOfWorkBase<IAppDbContext>
    {
    }
}