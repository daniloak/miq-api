using Omini.Miq.Domain.Common;

namespace Omini.Miq.Domain.Repositories;

public interface IRespositoryDocumentRowEntity<TEntity> : IDisposable where TEntity : DocumentRowEntity
{
    void Delete(TEntity entity, CancellationToken cancellationToken = default);
}