using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.User;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.UserAggregate.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.UserAggregate.User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired(false);
        builder.Property(x => x.Address).HasMaxLength(400).IsRequired(false);

        builder.HasMany(x => x.BorrowedBooks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}
