using AITasker_Modular.Database;
using Microsoft.EntityFrameworkCore;
using AITasker_Modular.Modules.JobModule.DTOs;

namespace AITasker_Modular.Modules.JobModule;

public class JobService : IJobService
{
    private readonly DataContext _context;

    public JobService(DataContext context)
    {
        _context = context;
    }

    public async Task<JobPost> CreateJobAsync(CreateJobPostDto jobPostDto)
    {
        var jobPost = new JobPost
        {
            Id = Guid.NewGuid().ToString(), // JobPost.Id is already string
            ClientId = jobPostDto.ClientId,
            Title = jobPostDto.Title.Trim(),
            Description = jobPostDto.Description.Trim(),
            Budget = jobPostDto.Budget,
            Deadline = jobPostDto.Deadline,
            Status = "Open", 
            CreatedAt = DateTime.UtcNow,
            AICategoryDomainId = jobPostDto.AICategoryDomainId
        };

        if (jobPostDto.SkillIds != null && jobPostDto.SkillIds.Any())
        {
            var skills = await _context.Skills
                                       .Where(s => jobPostDto.SkillIds.Contains(s.Id)) // Skill.Id is now string, so direct comparison is fine
                                       .ToListAsync();
            foreach (var skill in skills)
            {
                jobPost.Skills.Add(skill);
            }
        }

        _context.JobPosts.Add(jobPost);
        await _context.SaveChangesAsync();
        return jobPost;
    }

    public async Task<IReadOnlyList<JobPost>> GetJobsAsync()
    {
        return await _context.JobPosts
                             .Include(jp => jp.AICategoryDomain) 
                             .Include(jp => jp.Skills) 
                             .AsNoTracking() 
                             .ToListAsync();
    }

    public async Task<JobPost?> GetJobPostByIdAsync(string id)
    {
        return await _context.JobPosts
                             .Include(jp => jp.AICategoryDomain)
                             .Include(jp => jp.Skills)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(jp => jp.Id == id);
    }
}