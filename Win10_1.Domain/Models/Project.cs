using System.ComponentModel.DataAnnotations;
using Win10_1.Domain.Enums;

namespace Win10_1.Domain.Models;

public class Project
{
    [Key]
    public int          Id              { get; set; }
    public string       ProjectName     { get; set; } = null!;
    public string?      Theme           { get; set; } = null!;
    public string?      Description     { get; set; } = null!;
    public int          PercentComplete { get; set; }
    public Priority     Priority        { get; set; }
    public Status       Status          { get; set; }
    public DateTime?    DeadLine        { get; set; }
    public DateTime     CreatedAt       { get; set; }
    public DateTime     UpdatedAt       { get; set; }

    public ICollection<ProjectTask>     Tasks       = new List<ProjectTask>();
    public ICollection<ProjectComment>  Comments    = new List<ProjectComment>();

    public int          UserId          { get; set; }
    public User         User            { get; set; } = null!;
}