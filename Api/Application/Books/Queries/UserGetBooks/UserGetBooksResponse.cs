using Application.Books.Queries.AdminGetFilteredBooks;

namespace Application.Books.Queries.UserGetBooks;

public class UserGetBooksResponse 
{
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public required List<UserBookItem> Items { get; set; }
}

public class UserBookItem
{
    public required string Title { get; set; }
    public required string AuthorFirstName { get; set; }
    public required string AuthorLastName { get; set; }
    public required string[] CategorieTitles { get; set; }
}

