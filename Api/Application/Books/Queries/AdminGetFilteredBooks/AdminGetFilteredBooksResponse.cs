using Application.Books.Queries.AdminGetBooks;

namespace Application.Books.Queries.AdminGetFilteredBooks;

public class AdminGetFilteredBooksResponse
{
    public required List<AdminFilteredBookItem> Items { get; set; }
}
public class AdminFilteredBookItem
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string AuthorFirstName { get; set; }
    public required string AuthorLastName { get; set; }
    public required string[] CategorieTitles { get; set; }
}
