using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Api.Dtos;

public sealed record PromissoryOutputDto
{
    public Guid Id { get; set; }
    public long Number { get; set; }
    public double Total { get; private set; }
    public PromissoryStatus Status { get; private set; }
    public string ExternalStatus { get; private set; }
    public List<PromissoryItemOutputDto> Items { get; set; }

    public sealed record PromissoryItemOutputDto
    {
        public int LineId { get; set; }
        public int LineOrder { get; set; }
        public string ItemCode { get; private set; }
        public string ItemName { get; private set; }
        public double Quantity { get; private set; }
        public double Price { get; private set; }
    }
}