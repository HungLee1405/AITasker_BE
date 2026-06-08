using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.ChatModule;

[Table("Messages")]
public class Message
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string ConversationId { get; set; } = string.Empty; // Changed Guid to string
    public string SenderId { get; set; } = string.Empty; // Changed Guid to string
    [Required]
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public Conversation? Conversation { get; set; }
    public ApplicationUser? Sender { get; set; }
}
