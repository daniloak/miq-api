using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Externals.Models;
using Omini.Miq.Domain.Externals.Repositories;

namespace Omini.Miq.Infrastructure.Externals.Repositories;

internal sealed class CompanyRepository : ICompanyRepository
{
    private readonly MiqContext _context;

    public CompanyRepository(MiqContext context)
    {
        _context = context;
    }

    public async Task<Company?> GetByTaxId(string taxId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Company>().FromSqlInterpolated($@"
                    SELECT 
                        CardCode as externalID, CardName as name, E_Mail as email, Address as address, ZipCode as zipCode 
                    FROM OCRD   
                    WHERE 
                        REPLACE(REPLACE(REPLACE(U_Cnpj, '-', ''), '.', ''), '/', '') = {taxId} AND
                        CardType = 'C' AND
                        U_ActiveMiq =1")
                    .SingleOrDefaultAsync(cancellationToken);
    }
}