using System.ComponentModel.DataAnnotations;

namespace Win10_1.Domain.Models;

public class ProjectComment
{
    [Key]
    public int      Id          { get; set; }
    public string   Author      { get; set; } = null!;
    public string   Comment     { get; set; } = null!;
    public DateTime CreatedAt   { get; set; }
    public DateTime UpdatedAt   { get; set; }

    public int      ProjectId   { get; set; }
    public Project  Project     { get; set; } = null!;
}
