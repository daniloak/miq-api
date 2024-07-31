using Omini.Miq.Domain.Common;

namespace Omini.Miq.Domain.Sales;

public class Promissory : DocumentEntity
{
    public float TotalAmount { get; private set; }
    public string Status { get;  private set; }
    public string ExternalStatus { get; private set; }
    public string Company { get; private set; }
    private List<PromissoryItem> _items = [];
    public IReadOnlyCollection<PromissoryItem> Items => _items;
}

public class PromissoryItem : DocumentRowEntity
{
    public float Quantity { get; set; }
    public float Price { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public float TotalAmount { get; set; }
}