using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.ChatModule;
using AITasker_Modular.Modules.JobModule;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.ProjectModule;

[Table("Projects")]
public class Project
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string? JobPostId { get; set; } // Changed Guid? to string?
    public string ClientId { get; set; } = string.Empty; // Changed Guid to string
    public string ExpertId { get; set; } = string.Empty; // Changed Guid to string
    public decimal EscrowBalance { get; set; }
    [Required]
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ProjectLink { get; set; }
    public string? ConversationId { get; set; } // Changed Guid? to string?

    public JobPost? JobPost { get; set; }
    public ApplicationUser? Client { get; set; }
    public ApplicationUser? Expert { get; set; }
    public Conversation? Conversation { get; set; }
}
