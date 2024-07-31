namespace Omini.Miq.Domain.Common;

public interface ISoftDeletable
{
  public int? DeletedBy { get; set; }
  public DateTime? DeletedOn { get; set; }
  public bool IsDeleted { get; set; }
}