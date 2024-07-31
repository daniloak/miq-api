using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Domain.Repositories;

public interface IPromissoryRepository : IRespositoryDocumentEntity<Promissory>
{
}

public interface IPromissoryItemRepository : IRespositoryDocumentRowEntity<PromissoryItem>
{
}