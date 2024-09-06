using Application.Common;
using MediatR;

namespace Application.Books.Queries.AdminGetBooks;

public class AdminGetBooksQuery : IRequest<Result<AdminGetBooksResponse>>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
