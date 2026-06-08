using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.ProjectModule;

[Table("MiniTasks")]
public class MiniTask
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty; // Changed Guid to string
    [Required]
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public string? FeedbackContent { get; set; }
    public string? FeedbackSenderId { get; set; } // Changed Guid? to string?
    public DateTime CreatedAt { get; set; }

    public Task? Task { get; set; }
    public ApplicationUser? FeedbackSender { get; set; }
}
