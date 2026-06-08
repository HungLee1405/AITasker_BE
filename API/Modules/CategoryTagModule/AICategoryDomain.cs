using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AITasker_Modular.Modules.CategoryTagModule;

[Table("AICategoryDomains")]
public class AICategoryDomain
{
    [Key] // Changed Guid to string
    public string Id { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
}
