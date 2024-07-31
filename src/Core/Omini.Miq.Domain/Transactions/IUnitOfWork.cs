namespace Omini.Miq.Domain.Transactions;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken = default);
}