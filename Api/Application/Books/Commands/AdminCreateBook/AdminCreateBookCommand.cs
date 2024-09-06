using Application.Common;
using MediatR;

namespace Application.Books.Commands.AdminCreateBook;

public class AdminCreateBookCommand : IRequest<Result>
{
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public required int AuthorId { get; set; }
    public required int[] CategoryIds { get; set; }
}
