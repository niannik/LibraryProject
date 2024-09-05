using Application.Common;
using MediatR;

namespace Application.Auth.Commands.AdminSignIn;

public class AdminSignInCommand : IRequest<Result<AdminSignInResponse>>
{
    public required string Password { get; set; }
    public required string Email { get; set; }
}
