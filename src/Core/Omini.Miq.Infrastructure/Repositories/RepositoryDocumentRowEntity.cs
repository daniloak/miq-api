using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Common;
using Omini.Miq.Domain.Repositories;

namespace Omini.Miq.Infrastructure;

internal abstract class RepositoryDocumentRowEntity<TEntity> : IRespositoryDocumentRowEntity<TEntity> where TEntity : DocumentRowEntity
{
    protected readonly MiqContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected RepositoryDocumentRowEntity(MiqContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public virtual void Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}