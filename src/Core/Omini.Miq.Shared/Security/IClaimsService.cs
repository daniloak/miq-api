using System.Security.Claims;

namespace Omini.Miq.Shared.Services.Security;

public interface IClaimsService
{
    ClaimsPrincipal ClaimsPrincipal { get; }
    int? MiqUserId { get; }
    string? Email { get; }
}