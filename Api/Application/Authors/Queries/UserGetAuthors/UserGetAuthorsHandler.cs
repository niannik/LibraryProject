using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Queries.UserGetAuthors;

public class UserGetAuthorsHandler : IRequestHandler <UserGetAuthorsQuery, Result<UserGetAuthorsResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public UserGetAuthorsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<UserGetAuthorsResponse>> Handle(UserGetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var response = new UserGetAuthorsResponse
        {
            Items = await _dbContext.Authors.Select(x => new UserAuthorItem
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToListAsync(cancellationToken)
        };

        return response;
    }
}
