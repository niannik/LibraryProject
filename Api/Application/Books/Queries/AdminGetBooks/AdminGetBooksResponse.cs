namespace Application.Books.Queries.AdminGetBooks;

public class AdminGetBooksResponse
{
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRecords { get; set; }
    public required List<AdminBookItem> Items { get; set; }
    
}

public class AdminBookItem
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string AuthorFirstName { get; set; }
    public required string AuthorLastName { get; set; }
    public required string[] CategorieTitles { get; set; }
}
