namespace Application.Authors.Queries.UserGetAuthors;

public class UserGetAuthorsResponse
{
    public required List<UserAuthorItem> Items { get; set; }
}

public class UserAuthorItem
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
