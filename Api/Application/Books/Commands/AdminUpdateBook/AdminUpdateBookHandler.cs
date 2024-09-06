using Application.Books.Common;
using Application.Categories.Common;
using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities.BookCategoryAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Commands.AdminUpdateBook;

public class AdminUpdateBookHandler : IRequestHandler<AdminUpdateBookCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminUpdateBookHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(AdminUpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (book == null)
        {
            return BookErrors.BookNotFound;
        }

        book.Title = request.Title;
        book.AuthorId = request.AuthorId;
        book.PublicationYear = request.PublicationYear;

        var oldCategories = book.BookCategories
            .Select(x => x.CategoryId).ToArray();

        var addedCategoriesId = request.CategoryIds.Except(oldCategories);
       
        foreach (var categoryId in addedCategoriesId)
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

        var deletedCategoriesId = oldCategories.Except(request.CategoryIds);
        foreach (var categoryId in deletedCategoriesId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
            if (category == null)
            {
                return CategoryErrors.CategoyNotFound;
            }
            var bookCategory = category.BookCategories.Where(x => x.BookId == categoryId).FirstOrDefault();
            _dbContext.BookCategories.Remove(bookCategory);

        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
