using Omini.Miq.Domain.Common;

namespace Omini.Miq.Domain.Sales;

public class Promissory : DocumentEntity
{
    public double Total { get; private set; }
    public PromissoryStatus Status { get; private set; }
    public string ExternalStatus { get; private set; }
    private List<PromissoryItem> _items = [];
    public IReadOnlyCollection<PromissoryItem> Items => _items;

    private int LastLineOrder => _items.Any() ? _items.Max(p => p.LineOrder) + 1 : 0;
    private int LastLineId => _items.Any() ? _items.Max(p => p.LineId) + 1 : 0;

    private void CalculateTotal()
    {
        Total = _items.Any() ? _items.Sum(x => x.LineTotal) : 0;
    }

    public Promissory()
    {
        Status = PromissoryStatus.Open;
    }

    public Promissory AddItem(string itemCode, string itemName, double quantity, double price, int? lineOrder = null)
    {
        var newItem = new PromissoryItem(
            documentId: Id,
            lineOrder: lineOrder ?? LastLineOrder,
            lineId: LastLineId,
            itemCode: itemCode,
            itemName: itemName,
            quantity: quantity,
            price: price
        );

        _items.Add(newItem);

        CalculateTotal();

        return this;
    }

    public Promissory Cancel()
    {
        Status = PromissoryStatus.Canceled;

        return this;
    }
}

public class PromissoryItem : DocumentRowEntity
{
    public string ItemCode { get; private set; }
    public string ItemName { get; private set; }
    public double Quantity { get; private set; }
    public double Price { get; private set; }
    public double LineTotal => Quantity * Price;

    public PromissoryItem(long documentId, int lineId, string itemCode, string itemName, double quantity, double price, int? lineOrder = null)
    {
        DocumentId = documentId;
        LineId = lineId;

        if (lineOrder is not null)
        {
            LineOrder = lineOrder.Value;
        }

        ItemCode = itemCode;
        ItemName = itemName;
        Quantity = quantity;
        Price = price;
    }
}