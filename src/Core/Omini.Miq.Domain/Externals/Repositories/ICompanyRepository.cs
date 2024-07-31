using Omini.Miq.Domain.Externals.Models;

namespace Omini.Miq.Domain.Externals.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetByTaxId(string taxId, CancellationToken cancellationToken = default);
}