using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.CategoryTagModule;

public class AICategoryDomainExpertProfile
{
    public string AICategoryDomainsId { get; set; } = string.Empty; // Changed Guid to string
    public string ExpertProfilesUserId { get; set; } = string.Empty; // Changed Guid to string

    public AICategoryDomain? AICategoryDomain { get; set; }
    public ExpertProfile? ExpertProfile { get; set; }
}
