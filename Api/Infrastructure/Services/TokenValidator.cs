using Application.Common;
using Application.Common.Interfaces;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace Infrastructure.Services;

public class TokenValidator
{
    private readonly IApplicationDbContext _dbContext;

    public TokenValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ValidateSecurityStamp(IPrincipal? principal)
    {
        if (int.TryParse(principal?.Identity?.GetUserClaimValue(ClaimTypes.NameIdentifier), out var userId) == false)
            return false;

        return true;
    }
}
