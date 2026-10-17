using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Business.Data.Configurations;

public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
{
    public void Configure(EntityTypeBuilder<ProjectComment> builder)
    {
        builder.ToTable("comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Author)
               .IsRequired()
               .HasMaxLength(CommentConstraints.AuthorMaxLength);

        builder.Property(c => c.Comment)
               .IsRequired()
               .HasMaxLength(CommentConstraints.CommentMaxLength);

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.UpdatedAt) 
               .IsRequired();

        builder.HasOne(c => c.Project)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
