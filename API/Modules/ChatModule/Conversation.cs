using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.JobModule;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.ChatModule;

[Table("Conversations")]
public class Conversation
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string? OriginJobPostId { get; set; } // Changed Guid? to string?
    public string ClientId { get; set; } = string.Empty; // Changed Guid to string
    public string ExpertId { get; set; } = string.Empty; // Changed Guid to string
    public DateTime CreatedAt { get; set; }

    public JobPost? OriginJobPost { get; set; }
    public ApplicationUser? Client { get; set; }
    public ApplicationUser? Expert { get; set; }
}
