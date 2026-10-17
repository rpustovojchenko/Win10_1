using System.ComponentModel.DataAnnotations;
namespace Win10_1.Domain.Models;

public class ProjectTask
{
    [Key]
    public int      Id          { get; set; }
    public string   Title       { get; set; } = null!;
    public string   Description { get; set; } = null!;
    public DateTime CreatedAt   { get; set; }
    public DateTime UpdatedAt   { get; set; }

    public int      ProjectId   { get; set; }
    public Project  Project     { get; set; } = null!;
}
