namespace Omini.Miq.Domain.Common;

public abstract class MasterEntity : IAuditable
{
    public string Code { get; protected set; }
    public string Name { get; protected set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}