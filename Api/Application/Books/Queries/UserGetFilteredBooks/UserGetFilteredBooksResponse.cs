using Application.Books.Queries.AdminGetFilteredBooks;

namespace Application.Books.Queries.UserGetFilteredBooks;

public class UserGetFilteredBooksResponse
{
    public required List<UserFilteredBookItem> Items { get; set; }
}

public class UserFilteredBookItem
{
    public required string Title { get; set; }
    public required string AuthorFirstName { get; set; }
    public required string AuthorLastName { get; set; }
    public required string[] CategorieTitles { get; set; }
}
