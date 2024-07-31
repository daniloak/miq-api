using System.Security.Claims;
using Omini.Miq.Shared.Extensions;
using Omini.Miq.Shared.Services.Security;

namespace Omini.Miq.Api.Services.Security;

internal class ClaimsService : IClaimsService
{
    private readonly IHttpContextAccessor _accessor;

    public ClaimsService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public ClaimsPrincipal ClaimsPrincipal => _accessor.HttpContext.User;

    public string? GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetEmail() : null;
    }

    public int? MiqUserId => GetMiqUserId();
    public string? Email => GetEmail();

    private int? GetMiqUserId()
    {
        return IsAuthenticated() ? ClaimsPrincipal.GetMiqUserId() : null;
    }

    private string? GetEmail()
    {
        return IsAuthenticated() ? ClaimsPrincipal.GetEmail() : null;
    }

    private bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
