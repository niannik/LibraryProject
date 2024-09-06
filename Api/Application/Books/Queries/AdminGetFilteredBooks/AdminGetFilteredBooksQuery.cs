using Application.Common;
using MediatR;

namespace Application.Books.Queries.AdminGetFilteredBooks;

public class AdminGetFilteredBooksQuery : IRequest<Result<AdminGetFilteredBooksResponse>>
{
    public string? TitleFilter { get; set; }
    public string? AuthorNameFilter { get; set; }
}
