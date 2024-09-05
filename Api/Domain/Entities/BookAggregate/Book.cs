using Domain.Common;
using Domain.Entities.AuthorAggregate;
using Domain.Entities.BookCategoryAggregate;
using Domain.Entities.BorrowedBookAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.BookAggregate;

public class Book : AuditableEntity, ISoftDelete
{
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required int AuthorId { get; set; }

    #region Relations
    public Author? Author { get; set; }
    public ICollection<BookCategory>? BookCategories { get; set; }
    public ICollection<BorrowedBook>? BorrowedBooks { get; set; }
     #endregion
}
