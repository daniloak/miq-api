using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Infrastructure.Repositories;

internal sealed class PromissoryRepository : RepositoryDocumentEntity<Promissory>, IPromissoryRepository
{
    public PromissoryRepository(MiqContext context) : base(context)
    {
    }

    public override async Task<Promissory?> GetById(long id, CancellationToken cancellationToken = default)
    {
        var promissory = await DbSet.Include(p => p.Items)
                          .Where(p => p.Id == id)
                          .SingleOrDefaultAsync(cancellationToken);

        if (promissory is not null)
        {
            //quotation.PayingSource = new PayingSource() { Name = GetPaymentSource(quotation) };
        }

        return promissory;
    }  
}


internal class PromissoryItemRepository : RepositoryDocumentRowEntity<PromissoryItem>, IPromissoryItemRepository
{
    public PromissoryItemRepository(MiqContext context) : base(context)
    {
    }
}