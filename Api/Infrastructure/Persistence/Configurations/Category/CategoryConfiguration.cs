using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Category
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.CategoryAggregate.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.CategoryAggregate.Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.BookCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(false);
        }
    }
}
