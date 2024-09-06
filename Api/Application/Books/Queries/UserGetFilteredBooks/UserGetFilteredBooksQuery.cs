using Application.Common;
using MediatR;

namespace Application.Books.Queries.UserGetFilteredBooks;

public class UserGetFilteredBooksQuery : IRequest<Result<UserGetFilteredBooksResponse>>
{
    public string? TitleFilter { get; set; }
    public string? AuthorNameFilter { get; set; }
}
