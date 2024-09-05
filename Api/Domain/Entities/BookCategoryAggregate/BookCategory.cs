using Domain.Entities.BookAggregate;
using Domain.Entities.CategoryAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.BookCategoryAggregate;

public class BookCategory : Entity
{
    public BookCategory() { }
    public required int BookId { get; set; }
    public required int CategoryId { get; set; }
    public Book? Book { get; set; }
    public Category? Category { get; set; }
}
