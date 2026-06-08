using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.JobModule;

[Table("Proposals")]
public class Proposal
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string JobPostId { get; set; } = string.Empty; // Changed Guid to string
    public string ExpertId { get; set; } = string.Empty; // Changed Guid to string
    public decimal BidAmount { get; set; }
    [Required]
    public string CoverLetter { get; set; } = string.Empty;
    [Required]
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public JobPost? JobPost { get; set; }
    public ApplicationUser? Expert { get; set; }
}
