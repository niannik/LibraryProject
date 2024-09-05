using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Book;

public class BookConfigurations : IEntityTypeConfiguration<Domain.Entities.BookAggregate.Book>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.BookAggregate.Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PublicationYear).HasMaxLength(4).IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId)
            .IsRequired();
        builder.HasMany(x => x.BookCategories)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.BookId)
            .IsRequired();
        builder.HasMany(x => x.BorrowedBooks)
            .WithOne(x => x.Book)
            .HasForeignKey(x => x.BookId)
            .IsRequired();
    }
}
