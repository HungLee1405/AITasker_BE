using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITasker_Modular.Modules.ProjectModule;

[Table("Tasks")]
public class Task
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty; // Changed Guid to string
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Status { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public Project? Project { get; set; }
}
