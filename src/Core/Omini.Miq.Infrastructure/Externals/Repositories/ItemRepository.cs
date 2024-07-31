using Microsoft.EntityFrameworkCore;
using Omini.Miq.Domain.Externals.Models;
using Omini.Miq.Domain.Externals.Repositories;

namespace Omini.Miq.Infrastructure.Externals.Repositories;

internal sealed class ItemRepository : IItemRepository
{
    private readonly MiqContext _context;

    public ItemRepository(MiqContext context)
    {
        _context = context;
    }

    public async Task<Item?> GetByCode(string itemCode, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Item>().FromSqlInterpolated($@"
                   SELECT
                        T0.[ItemCode] AS Code,
                        T2.[ItemName] AS Name,
                        T2.[U_MIN_VENDA] AS MinimumQuantityMultiplier,
                        CAST(CASE WHEN T0.[U_BO] = 'S' THEN 1 ELSE 0 END AS BIT) AS BackOrder,
                        T2.[U_Vales] AS OnPromissory,
                        T2.[FrgnName] AS Application,
                        (T0.[OnHand] - T0.[IsCommited]) AS Quantity,
                        T1.[Price] AS Price,
                        T0.[WhsCode] as Warehouse,
                        CAST(CASE WHEN ((T0.[OnHand] - T0.[IsCommited]) > 0) AND (T0.[U_BO] <> 'S') THEN 1 ELSE 0 END AS BIT) as Available
                    FROM
                            OITW T0
                        INNER JOIN
                            ITM1 T1 ON T0.[ItemCode] = T1.[ItemCode]
                        INNER JOIN
                            OITM T2 ON T0.[ItemCode] = T2.[ItemCode]
                        WHERE
                            T0.[WhsCode] in ('01','03')
                        AND
                            T0.[ItemCode] = {itemCode}
                        AND
                            T1.[PriceList] = (SELECT ListNum FROM OPLN WHERE U_ListaIPAQ = 1)")
                    .SingleOrDefaultAsync(cancellationToken);
    }
}