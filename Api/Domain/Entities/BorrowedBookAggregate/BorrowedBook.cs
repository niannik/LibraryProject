using Domain.Entities.BookAggregate;
using Domain.Entities.Common;
using Domain.Entities.UserAggregate;

namespace Domain.Entities.BorrowedBookAggregate;

public class BorrowedBook : AuditableEntity
{
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public required int BookId { get; set; }
    public required int UserId { get; set; }

    #region Relations
    public Book? Book { get; set; }
    public User? User { get; set; }
    #endregion
}
