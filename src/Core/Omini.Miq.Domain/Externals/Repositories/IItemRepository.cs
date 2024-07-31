using Omini.Miq.Domain.Externals.Models;

namespace Omini.Miq.Domain.Externals.Repositories;

public interface IItemRepository {
    Task<Item?> GetByCode(string code, CancellationToken cancellationToken = default);
}