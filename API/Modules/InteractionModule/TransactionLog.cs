using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AITasker_Modular.Modules.ProjectModule;
using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.InteractionModule;

[Table("TransactionLogs")]
public class TransactionLog
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    public string? ProjectId { get; set; } // Changed Guid? to string?
    public string? SourceWalletId { get; set; } // Changed Guid? to string?
    public string? DestinationWalletId { get; set; } // Changed Guid? to string?
    public decimal Amount { get; set; }
    [Required]
    public string Type { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Project? Project { get; set; }
    public Wallet? SourceWallet { get; set; }
    public Wallet? DestinationWallet { get; set; }
}
