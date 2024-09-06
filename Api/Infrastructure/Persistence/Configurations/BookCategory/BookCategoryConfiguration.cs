using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.BookCategory;

public class BookCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.BookCategoryAggregate.BookCategory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.BookCategoryAggregate.BookCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Book)
            .WithMany(x => x.BookCategories)
            .HasForeignKey(x => x.BookId)
            .IsRequired(false);
        builder.HasOne(x => x.Category)
            .WithMany(x => x.BookCategories)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(false);
    }
}
