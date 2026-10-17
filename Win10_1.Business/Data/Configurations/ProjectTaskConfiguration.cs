using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Configurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(TaskConstraints.TitleMaxLength);

        builder.Property(t => t.Description)
               .IsRequired()
               .HasMaxLength(TaskConstraints.DescriptionMaxLength);

        builder.Property(t => t.CreatedAt)
               .IsRequired();

        builder.Property(t => t.UpdatedAt)
               .IsRequired();

        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
