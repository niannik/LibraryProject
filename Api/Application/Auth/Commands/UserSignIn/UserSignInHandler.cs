using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Commands.UserSignIn;

public class UserSignInHandler : IRequestHandler<UserSignInCommand, Result<UserSignInResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly AdminInfo _adminInfo;
    private readonly ITokenFactoryService _tokenFactoryService;
    public UserSignInHandler(IApplicationDbContext dbContext, AdminInfo adminInfo, ITokenFactoryService tokenFactoryService)
    {
        _adminInfo = adminInfo;
        _dbContext = dbContext;
        _tokenFactoryService = tokenFactoryService;
    }
    public async Task<Result<UserSignInResponse>> Handle(UserSignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (request.Password != user.Password || request.Email != user.Email || user is null)
            return AuthErrors.InvalidAdminLogin;

        user.LastLoginDate = DateTime.Now;
        user.LastUpdatedAt = DateTime.Now;

        var response = new UserSignInResponse
        {
            JwtToken = _tokenFactoryService.CreateJwt(user),
            RefreshToken = _tokenFactoryService.CreateRefreshToken()
        };

        return response;
    }
}
