namespace Omini.Miq.Domain.Common;

public interface IAuditable
{
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int? UpdatedBy { get; set; }
  public DateTime? UpdatedOn { get; set; }
}
