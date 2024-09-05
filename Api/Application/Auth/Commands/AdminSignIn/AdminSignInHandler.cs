using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Commands.AdminSignIn;

public class AdminSignInHandler : IRequestHandler<AdminSignInCommand, Result<AdminSignInResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly AdminInfo _adminInfo;
    private readonly ITokenFactoryService _tokenFactoryService;
    public AdminSignInHandler(IApplicationDbContext dbContext , AdminInfo adminInfo, ITokenFactoryService tokenFactoryService)
    {
        _adminInfo = adminInfo;
        _dbContext = dbContext;
        _tokenFactoryService = tokenFactoryService;
    }
    public async Task<Result<AdminSignInResponse>> Handle(AdminSignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email
        && request.Email == _adminInfo.Email);

        if (request.Password != _adminInfo.Password || request.Email != _adminInfo.Email || user is null)
            return AuthErrors.InvalidAdminLogin;

        var response = new AdminSignInResponse
        {
            JwtToken = _tokenFactoryService.CreateJwt(user),
            RefreshToken = _tokenFactoryService.CreateRefreshToken()
        };

        return response;
    }
}
