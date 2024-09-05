using Domain.Entities.BookCategoryAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.CategoryAggregate;

public class Category : AuditableEntity
{
    public required string Title { get; set; }

    #region Relations
    public ICollection<BookCategory>? BookCategories { get; set; }
    #endregion
}