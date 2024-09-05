using Domain.Common;
using Domain.Entities.BorrowedBookAggregate;
using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.UserAggregate;

public class User : AuditableEntity, ISoftDelete
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime LastLoginDate { get; set; }
    public required RoleTypes RoleType {get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    #region Relations
    public ICollection<BorrowedBook>? BorrowedBooks { get; set; }
    #endregion
}
