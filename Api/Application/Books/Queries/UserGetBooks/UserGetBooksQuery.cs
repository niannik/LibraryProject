using Application.Common;
using MediatR;

namespace Application.Books.Queries.UserGetBooks;

public class UserGetBooksQuery : IRequest<Result<UserGetBooksResponse>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
