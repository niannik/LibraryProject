using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    ClaimsPrincipal? User { get; }
}
