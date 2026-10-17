using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProjectName)
               .IsRequired()
               .HasMaxLength(ProjectConstraints.ProjectNameMaxLength);

        builder.Property(p => p.Description)
               .HasMaxLength(ProjectConstraints.DescriptionMaxLength);

        builder.Property(p => p.Theme)
               .HasMaxLength(ProjectConstraints.ThemeMaxLength);

        builder.Property(p => p.PercentComplete)
               .IsRequired();

        builder.Property(p => p.Priority)
               .IsRequired()
               .HasConversion<int>();

        builder.Property(p => p.Status)
               .IsRequired()
               .HasConversion<int>();

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.UpdatedAt)
               .IsRequired();

        builder.HasOne(p => p.User)
               .WithMany(u => u.Projects)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
