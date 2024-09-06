using Application.Categories.Common;
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.BookAggregate;
using Domain.Entities.BookCategoryAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.AdminCreateBook;

public class AdminCreateBookHandler : IRequestHandler<AdminCreateBookCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminCreateBookHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AdminCreateBookCommand request, CancellationToken cancellationToken)
    {
        
        var book = new Book
        {
            Title = request.Title,
            PublicationYear = request.PublicationYear,
            AuthorId = request.AuthorId,
        };
        _dbContext.Books.Add(book);

        foreach (var categoryId in request.CategoryIds)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
            if (category == null)
            {
                return CategoryErrors.CategoyNotFound;
            }

            var bookCategory = new BookCategory
            {
                Book = book,
                CategoryId = categoryId,
            };
            _dbContext.BookCategories.Add(bookCategory);
        }

        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
