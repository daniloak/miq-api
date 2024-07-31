using Omini.Miq.Domain.Transactions;

namespace Omini.Miq.Infrastructure.Transaction;

internal class UnitOfWork : IUnitOfWork
{
    private readonly MiqContext _miqContext;
    public UnitOfWork(MiqContext miqContext)
    {
        _miqContext = miqContext;
    }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        await _miqContext.SaveChangesAsync(cancellationToken);
    }
}