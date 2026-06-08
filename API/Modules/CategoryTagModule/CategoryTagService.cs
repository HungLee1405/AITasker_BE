namespace AITasker_Modular.Modules.CategoryTagModule;

public class CategoryTagService : ICategoryTagService
{
    public Task<IReadOnlyList<AICategoryDomain>> GetCategoriesAsync()
    {
        return Task.FromResult<IReadOnlyList<AICategoryDomain>>(new List<AICategoryDomain>());
    }

    public Task<IReadOnlyList<Skill>> GetSkillsAsync()
    {
        return Task.FromResult<IReadOnlyList<Skill>>(new List<Skill>());
    }
}
