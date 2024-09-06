using Application.Common;
using MediatR;

namespace Application.Books.Commands.AdminDeleteBook;

public class AdminDeleteBookCommand : IRequest<Result>
{
    public required int Id { get; set; }    
}
