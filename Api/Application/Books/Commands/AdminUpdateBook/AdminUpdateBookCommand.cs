using Application.Common;
using MediatR;

namespace Application.Books.Commands.AdminUpdateBook;

public class AdminUpdateBookCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public required int AuthorId { get; set; }
    public required int[] CategoryIds { get; set; }
}
