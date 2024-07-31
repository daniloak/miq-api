using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Infrastructure.Repositories;

internal class PromissoryRepository : RepositoryDocumentEntity<Promissory>, IPromissoryRepository
{
    public PromissoryRepository(MiqContext context) : base(context)
    {
    }

    public override async Task<Promissory?> GetById(long id, CancellationToken cancellationToken = default)
    {
        var quotation = await DbSet.Include(p => p.Items)
                          .Where(p => p.Id == id)
                          .SingleOrDefaultAsync(cancellationToken);

        if (quotation is not null)
        {
            //quotation.PayingSource = new PayingSource() { Name = GetPaymentSource(quotation) };
        }

        return quotation;
    }

    public override async Task<Promissory?> GetByNumber(long number, CancellationToken cancellationToken = default)
    {
        var quotation = await DbSet.Include(p => p.Items).AsNoTracking()
                          .Where(p => p.Number == number)
                          .SingleOrDefaultAsync(cancellationToken);

        if (quotation is not null)
        {
            //quotation.PayingSource = new PayingSource() { Name = GetPaymentSource(quotation) };
        }

        return quotation;
    }   
}


internal class PromissoryItemRepository : RepositoryDocumentRowEntity<PromissoryItem>, IPromissoryItemRepository
{
    public PromissoryItemRepository(MiqContext context) : base(context)
    {
    }
}