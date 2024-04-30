using QassimPrincipality.Domain.Interfaces;
using Framework.Core.Data.Uow;

namespace QassimPrincipality.Infrastructure.Data;

public sealed class UnitOfWork : UnitOfWorkBase<IAppDbContext>, IUnitOfWork
{
    public UnitOfWork(IAppDbContext context) : base(context)
    {
    }
}