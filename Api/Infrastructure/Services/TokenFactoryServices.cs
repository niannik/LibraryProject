using Application.Common.Interfaces;
using Application.Common.Settings;
using Domain.Entities.UserAggregate;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenFactoryService : ITokenFactoryService
{
    private readonly ISecurityService _securityService;
    private readonly BearerTokenSettings _bearerTokenSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenFactoryService(ISecurityService securityService, BearerTokenSettings bearerTokenSettings, IDateTimeProvider dateTimeProvider)
    {
        _securityService = securityService;
        _bearerTokenSettings = bearerTokenSettings;
        _dateTimeProvider = dateTimeProvider;
    }


    public string CreateRefreshToken()
        => _securityService.CreateRandomString();

    public string CreateJwt(User user)
    {
        var claims = CreateAccessTokenClaims(user);

        return CreateAccessToken(claims);
    }

    private static List<Claim> CreateAccessTokenClaims(User user)
    {
        var claims = new List<Claim>
            {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),

            };
        if(user.RoleType == RoleTypes.Admin)
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

        else
            claims.Add(new Claim(ClaimTypes.Role, "User"));

        return claims;
    }

    private static readonly JwtSecurityTokenHandler JwtHandler = new();

    private string CreateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerTokenSettings.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = _dateTimeProvider.Now;

        var token = new JwtSecurityToken(
                     issuer: _bearerTokenSettings.Issuer,
                     audience: _bearerTokenSettings.Audience,
                     claims: claims,
                     notBefore: now,
                     expires: now.AddMinutes(_bearerTokenSettings.AccessTokenExpirationMinutes),
                     signingCredentials: signingCredentials);

        return JwtHandler.WriteToken(token);

    }
}
