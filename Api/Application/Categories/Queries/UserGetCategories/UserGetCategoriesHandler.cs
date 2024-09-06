using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.UserGetCategories;

public class UserGetCategoriesHandler : IRequestHandler<UserGetCategoriesQuery, Result<UserGetCategoriesResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public UserGetCategoriesHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<UserGetCategoriesResponse>> Handle(UserGetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = new UserGetCategoriesResponse
        {
            Titles = await _dbContext.Categories.Select(x => x.Title).ToArrayAsync(cancellationToken)
        };

        return response;
    }
}
