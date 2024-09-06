namespace Application.Books.Commands.AdminUpdateBook;

public class AdminUpdateBookDto
{
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public required int AuthorId { get; set; }
    public required int[] CategoryIds { get; set; }
}
