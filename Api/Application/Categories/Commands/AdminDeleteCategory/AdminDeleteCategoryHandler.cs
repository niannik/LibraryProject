using Application.Categories.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.AdminDeleteCategory;

public class AdminDeleteCategoryHandler : IRequestHandler<AdminDeleteCategoryCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminDeleteCategoryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminDeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (category == null)
            return CategoryErrors.CategoyNotFound;

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
