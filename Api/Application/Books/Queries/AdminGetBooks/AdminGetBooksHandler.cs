using Application.Books.Common;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.Queries.AdminGetBooks;

public class AdminGetBooksHandler : IRequestHandler<AdminGetBooksQuery, Result<AdminGetBooksResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    public AdminGetBooksHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<AdminGetBooksResponse>> Handle(AdminGetBooksQuery request, CancellationToken cancellationToken)
    {
        var response = new AdminGetBooksResponse
        {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            Items = await _dbContext.Books.Select(x => new AdminBookItem
            {
                Id = x.Id,
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
