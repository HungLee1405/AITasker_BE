using AITasker_Modular.Modules.CategoryTagModule;

namespace AITasker_Modular.Modules.ProjectModule;

public class ProjectSkill
{
    public string ProjectsId { get; set; } = string.Empty; // Changed Guid to string
    public string SkillsId { get; set; } = string.Empty; // Changed Guid to string

    public Project? Project { get; set; }
    public Skill? Skill { get; set; }
}
