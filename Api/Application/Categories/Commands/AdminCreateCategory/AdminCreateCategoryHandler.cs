using Application.Categories.Common;
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.CategoryAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.AdminCreateCategory;

public class AdminCreateCategoryHandler : IRequestHandler<AdminCreateCategoryCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminCreateCategoryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminCreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _dbContext.Categories.AnyAsync(x => x.Title == request.Title);
        if (isExist)
            return CategoryErrors.CategoyIsAlreadyExist;

        var category = new Category { Title = request.Title };

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
