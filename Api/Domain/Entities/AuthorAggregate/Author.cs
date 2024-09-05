using Domain.Entities.BookAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.AuthorAggregate;

public class Author : AuditableEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }


    #region Relations
    public ICollection<Book>? Books { get; set; }
    #endregion
}
