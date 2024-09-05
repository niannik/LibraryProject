using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.UserAggregate;
using Domain.Enums;
using MediatR;

namespace Application.Auth.Commands.UserSignUp;

public class UserSignUpHandler : IRequestHandler<UserSignUpCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public UserSignUpHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            RoleType = RoleTypes.User,
            LastLoginDate = DateTime.Now,
            CreatedAt = DateTime.Now,
            LastUpdatedAt = DateTime.Now,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
