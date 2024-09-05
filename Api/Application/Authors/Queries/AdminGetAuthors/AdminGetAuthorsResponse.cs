using Domain.Entities.BookAggregate;

namespace Application.Authors.Queries.AdminGetAuthors;

public class AdminGetAuthorsResponse
{
    public required List<AdminAuthorItem> Items { get; set; }
}

public class AdminAuthorItem
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}