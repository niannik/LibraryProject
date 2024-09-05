using Application.Common.Interfaces;
using System.Security.Claims;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        string? userId;
        if (httpContext == null)
        {
            userId = null;
        }
        else
        {
            ClaimsPrincipal user = httpContext.User;
            userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        UserId = string.IsNullOrWhiteSpace(userId) ? null : int.Parse(userId);
        User = httpContextAccessor.HttpContext?.User;
    }

    public int? UserId { get; }
    public ClaimsPrincipal? User { get; }
}