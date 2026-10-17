using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(UserConstraints.UsernameMaxLength);

        builder.Property(u => u.Login)
               .IsRequired()
               .HasMaxLength(UserConstraints.LoginMaxLength);

        builder.Property(u => u.HashPassword)
               .IsRequired()
               .HasMaxLength(UserConstraints.HashPasswordMaxLength);

        builder.HasIndex(u => u.Login)
               .IsUnique();

        builder.HasMany(u => u.Projects)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
