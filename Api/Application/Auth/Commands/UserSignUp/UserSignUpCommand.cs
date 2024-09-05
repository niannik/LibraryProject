using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Auth.Commands.UserSignUp;

public class UserSignUpCommand : IRequest<Result>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
}
