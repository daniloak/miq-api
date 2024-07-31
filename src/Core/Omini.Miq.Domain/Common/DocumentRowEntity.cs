namespace Omini.Miq.Domain.Common;

public abstract class DocumentRowEntity
{
    public long DocumentId { get; protected set; }
    public int LineId { get; protected set; }
    public int LineOrder { get; protected set; }
}