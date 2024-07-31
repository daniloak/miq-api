namespace Omini.Miq.Domain.Common;

public abstract class DocumentEntity : IAuditable, ISoftDeletable
{
    public long Id { get; init; }
    public long Number { get; private set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
}