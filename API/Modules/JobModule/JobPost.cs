using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.CategoryTagModule;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.JobModule;

[Table("JobPosts")]
public class JobPost
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty; // Changed Guid to string
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? AICategoryDomainId { get; set; } // Changed Guid? to string?

    public ApplicationUser? Client { get; set; }
    public AICategoryDomain? AICategoryDomain { get; set; }
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
