using AITasker_Modular.Modules.CategoryTagModule;

namespace AITasker_Modular.Modules.JobModule;

public class JobPostSkill
{
    public string JobPostsId { get; set; } = string.Empty; // Changed Guid to string
    public string SkillsId { get; set; } = string.Empty; // Changed Guid to string

    public JobPost? JobPost { get; set; }
    public Skill? Skill { get; set; }
}
