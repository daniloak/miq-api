using Omini.Miq.Domain.Common;

namespace Omini.Miq.Domain.Authentication;

public class User : IAuditable, ISoftDeletable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public Guid UID { get; set; }
    public string ExternalId { get; set; }
    public Role Role { get; set; }
    public string TaxId { get; set; }
    public string Email { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }

    public User(string name, string email, string externalId)
    {
        Name = name;
        Email = email;
        ExternalId = externalId;
    }
}