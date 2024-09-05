using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.BorrowedBook;

public class BorrowedBookConfiguration : IEntityTypeConfiguration<Domain.Entities.BorrowedBookAggregate.BorrowedBook>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.BorrowedBookAggregate.BorrowedBook> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StartDate).IsRequired();

        builder.HasOne(x => x.Book)
            .WithMany(x => x.BorrowedBooks)
            .HasForeignKey(x => x.BookId)
            .IsRequired();
        builder.HasOne(x => x.User)
            .WithMany(x => x.BorrowedBooks)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}
