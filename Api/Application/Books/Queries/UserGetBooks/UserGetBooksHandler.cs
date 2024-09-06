using Application.Books.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Queries.UserGetBooks;

public class UserGetBooksHandler : IRequestHandler<UserGetBooksQuery, Result<UserGetBooksResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public UserGetBooksHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<UserGetBooksResponse>> Handle(UserGetBooksQuery request, CancellationToken cancellationToken)
    {
        var response = new UserGetBooksResponse
        {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            Items = await _dbContext.Books.Select(x => new UserBookItem
            {
                Title = x.Title,
                AuthorFirstName = x.Author.FirstName,
                AuthorLastName = x.Author.LastName,
                CategorieTitles = x.BookCategories.Select(x => x.Category.Title).ToArray()
            }).ToListAsync(),
        };
        response.TotalRecords = response.Items.Count;
        if (response.Items.Count == 0)
            return BookErrors.BookNotFound;

        return response;
    }
}
