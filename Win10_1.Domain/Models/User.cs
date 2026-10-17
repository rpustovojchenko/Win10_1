using System.ComponentModel.DataAnnotations;

namespace Win10_1.Domain.Models;

public class User
{
    [Key]
    public int      Id              { get; set; }
    public string   Username        { get; set; } = null!;
    public string   Login           { get; set; } = null!;
    public string   HashPassword    { get; set; } = null!;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
