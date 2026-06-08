using AITasker_Modular.Modules.UserModule;

namespace AITasker_Modular.Modules.CategoryTagModule;

public class ExpertProfileSkill
{
    public string ExpertProfilesUserId { get; set; } = string.Empty; // Changed Guid to string
    public string SkillsId { get; set; } = string.Empty; // Changed Guid to string

    public ExpertProfile? ExpertProfile { get; set; }
    public Skill? Skill { get; set; }
}
