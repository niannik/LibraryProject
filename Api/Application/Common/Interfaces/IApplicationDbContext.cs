using Domain.Entities.AuthorAggregate;
using Domain.Entities.BookAggregate;
using Domain.Entities.BookCategoryAggregate;
using Domain.Entities.BorrowedBookAggregate;
using Domain.Entities.CategoryAggregate;
using Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DatabaseFacade Database { get; }
}
