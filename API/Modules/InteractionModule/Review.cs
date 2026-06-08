using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.ProjectModule;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.InteractionModule;

[Table("Reviews")]
public class Review
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty; // Changed Guid to string
    public string CreatedById { get; set; } = string.Empty; // Changed Guid to string
    public string TargetUserId { get; set; } = string.Empty; // Changed Guid to string
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public Project? Project { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? TargetUser { get; set; }
}
