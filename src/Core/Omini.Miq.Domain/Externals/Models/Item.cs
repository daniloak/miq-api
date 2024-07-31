namespace Omini.Miq.Domain.Externals.Models;

public class Item
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
    public string Warehouse { get; set; }
}