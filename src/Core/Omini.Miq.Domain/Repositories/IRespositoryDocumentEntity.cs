using System.Linq.Expressions;
using Omini.Miq.Domain.Common;
using Omini.Miq.Shared.Common;
using Omini.Miq.Shared.Entities;

namespace Omini.Miq.Domain.Repositories;

public interface IRespositoryDocumentEntity<TEntity> : IDisposable where TEntity : DocumentEntity
{
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, string? orderByField = null, SortDirection sortDirection = SortDirection.Asc, string? queryField = null, string? queryValue = null, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByNumber(long number, CancellationToken cancellationToken = default);
    Task<TEntity?> GetById(long id, CancellationToken cancellationToken = default);
    Task<PagedResult<TEntity>> GetAll(int currentPage = 1, int pageSize = 100, string? orderByField = null, SortDirection sortDirection = SortDirection.Asc, string? queryField = null, string? queryValue = null, CancellationToken cancellationToken = default);
    void Update(TEntity entity, CancellationToken cancellationToken = default);
    void Delete(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}