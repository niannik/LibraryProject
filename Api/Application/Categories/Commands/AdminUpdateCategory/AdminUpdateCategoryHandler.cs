using Application.Categories.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.AdminUpdateCategory;

public class AdminUpdateCategoryHandler : IRequestHandler<AdminUpdateCategoryCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminUpdateCategoryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminUpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category =await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category == null)
            return CategoryErrors.CategoyNotFound;

        category.LastUpdatedAt = DateTime.UtcNow;
        category.Title = request.Title;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
