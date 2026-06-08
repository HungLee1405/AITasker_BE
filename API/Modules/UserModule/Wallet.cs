using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITasker_Modular.Modules.UserModule;

[Table("Wallets")]
public class Wallet
{
    [Key]
    public string UserId { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}
