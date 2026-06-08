using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITasker_Modular.Modules.UserModule;

[Table("ExpertProfiles")]
public class ExpertProfile
{
    [Key] // Changed Guid to string
    public string UserId { get; set; } = string.Empty;
    [Required]
    public string JobTitle { get; set; } = string.Empty;
    [Required]
    public string Major { get; set; } = string.Empty;
    public string? Certifications { get; set; }
    [Required]
    public string Bio { get; set; } = string.Empty;
    public string? PortfolioUrls { get; set; }
    public decimal ReputationCredit { get; set; }
    public string? Location { get; set; }
    public double SuccessRate { get; set; }
}
