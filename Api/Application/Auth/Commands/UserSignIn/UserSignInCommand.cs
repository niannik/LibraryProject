using Application.Common;
using MediatR;

namespace Application.Auth.Commands.UserSignIn;

public class UserSignInCommand : IRequest<Result<UserSignInResponse>>
{
    public required string Password { get; set; }
    public required string Email { get; set; }
}
