using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.AdminGetCategories;

public class AdminGetCategoriesHandler : IRequestHandler<AdminGetCategoriesQuery, Result<AdminGetCategoriesResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminGetCategoriesHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<AdminGetCategoriesResponse>> Handle(AdminGetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var response = new AdminGetCategoriesResponse
        {
            Items = await _dbContext.Categories.Select(x => new AdminCategoryItem
            {
                Id = x.Id,
                Title = x.Title,
            }).ToListAsync(cancellationToken)
        };

        return response;
    }
}
