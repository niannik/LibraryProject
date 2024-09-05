using System.Security.Claims;
using System.Security.Principal;

namespace Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static string? GetUserClaimValue(this IIdentity identity, string claimType)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        return claimsIdentity?.FindFirstValue(claimType);
    }

    private static string? FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        return identity?.FindFirst(claimType)?.Value;
    }
}
