using Application.Books.Common;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Queries.UserGetFilteredBooks;

public class UserGetFilteredBooksHandler : IRequestHandler<UserGetFilteredBooksQuery, Result<UserGetFilteredBooksResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public UserGetFilteredBooksHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<UserGetFilteredBooksResponse>> Handle(UserGetFilteredBooksQuery request, CancellationToken cancellationToken)
    {
        var response = new UserGetFilteredBooksResponse
        {
            Items = await _dbContext.Books
            .When(request.TitleFilter != null, x => x.Title.ToLower().Contains(request.TitleFilter!.ToLower()))
            .When(request.AuthorNameFilter != null, x => x.Author!.LastName.ToLower().Contains(request.AuthorNameFilter!.ToLower()))
            .Select(x => new UserFilteredBookItem
            {
                Title = x.Title,
                AuthorFirstName = x.Author!.FirstName,
                AuthorLastName = x.Author.LastName,
                CategorieTitles = x.BookCategories!.Select(x => x.Category!.Title).ToArray()
            }).ToListAsync(cancellationToken)
        };

        if (response.Items.Count == 0)
            return BookErrors.BookNotFound;

        return response;
    }
}
