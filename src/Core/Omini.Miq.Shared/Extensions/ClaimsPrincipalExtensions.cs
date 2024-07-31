using Omini.Miq.Shared.Constants;
using System.Security.Claims;

namespace Omini.Miq.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException("Claim userEmail not found", nameof(principal));
        }

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }

    public static int? GetMiqUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException("Claim MiqUserId not found", nameof(principal));
        }

        var claim = principal.FindFirst(MiqKeyRegisteredClaimNames.MiqUserId);

        if (int.TryParse(claim?.Value, out int result))
        {
            return result;
        }

        return null;
    }
}
