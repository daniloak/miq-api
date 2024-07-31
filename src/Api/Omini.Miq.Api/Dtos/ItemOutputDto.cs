using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Api.Dtos;

public sealed record ItemOutputDto
{
    public string Code {get;set;}
    public string Name { get; set; }
    public double Quantity { get; set; }
    public double MinimumQuantityMultiplier { get; set; }
    public bool BackOrder { get; set; }
    public double OnPromissory { get; set; }
    public string Application { get; set; }
    public double Price { get; set; }
    public bool Available { get; set; }
}