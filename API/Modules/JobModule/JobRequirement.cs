using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITasker_Modular.Modules.JobModule;

[Table("JobRequirements")]
public class JobRequirement
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string JobPostId { get; set; } = string.Empty; // Changed Guid to string
    [Required]
    public string UseCaseName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public JobPost? JobPost { get; set; }
}
