using Application.Books.Common;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Queries.AdminGetFilteredBooks;

public class AdminGetFilteredBooksHandler : IRequestHandler<AdminGetFilteredBooksQuery, Result<AdminGetFilteredBooksResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminGetFilteredBooksHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<AdminGetFilteredBooksResponse>> Handle(AdminGetFilteredBooksQuery request, CancellationToken cancellationToken)
    {
        var response = new AdminGetFilteredBooksResponse
        {
            Items = await _dbContext.Books
            .When(request.TitleFilter != null, x => x.Title.ToLower().Contains(request.TitleFilter!.ToLower()))
            .When(request.AuthorNameFilter != null, x => x.Author!.LastName.ToLower().Contains(request.AuthorNameFilter!.ToLower()))
            .Select(x => new AdminFilteredBookItem
            {
                Id = x.Id,
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
