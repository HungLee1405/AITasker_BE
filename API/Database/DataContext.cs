using AITasker_Modular.Modules.CategoryTagModule;
using AITasker_Modular.Modules.ChatModule;
using AITasker_Modular.Modules.InteractionModule;
using AITasker_Modular.Modules.JobModule;
using AITasker_Modular.Modules.ProjectModule;
using AITasker_Modular.Modules.UserModule;
using Microsoft.EntityFrameworkCore;
using ProjectTask = AITasker_Modular.Modules.ProjectModule.Task;

namespace AITasker_Modular.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ExpertProfile> ExpertProfiles { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<AICategoryDomain> AICategoryDomains { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobPost> JobPosts { get; set; }
    public DbSet<JobRequirement> JobRequirements { get; set; }
    public DbSet<Proposal> Proposals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; } 
    public DbSet<MiniTask> MiniTasks { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<TransactionLog> TransactionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);
        modelBuilder.Entity<ExpertProfile>().HasKey(x => x.UserId);
        modelBuilder.Entity<Wallet>().HasKey(x => x.UserId);
        modelBuilder.Entity<AICategoryDomain>().HasKey(x => x.Id);
        modelBuilder.Entity<Skill>().HasKey(x => x.Id);
        modelBuilder.Entity<JobPost>().HasKey(x => x.Id);
        modelBuilder.Entity<JobRequirement>().HasKey(x => x.Id);
        modelBuilder.Entity<Proposal>().HasKey(x => x.Id);
        modelBuilder.Entity<Project>().HasKey(x => x.Id);
        modelBuilder.Entity<ProjectTask>().HasKey(x => x.Id);
        modelBuilder.Entity<MiniTask>().HasKey(x => x.Id);
        modelBuilder.Entity<Conversation>().HasKey(x => x.Id);
        modelBuilder.Entity<Message>().HasKey(x => x.Id);
        modelBuilder.Entity<Review>().HasKey(x => x.Id);
        modelBuilder.Entity<TransactionLog>().HasKey(x => x.Id);

        modelBuilder.Entity<AICategoryDomainExpertProfile>().HasKey(x => new { x.AICategoryDomainsId, x.ExpertProfilesUserId });
        modelBuilder.Entity<ExpertProfileSkill>().HasKey(x => new { x.ExpertProfilesUserId, x.SkillsId });
        modelBuilder.Entity<JobPostSkill>().HasKey(x => new { x.JobPostsId, x.SkillsId });
        modelBuilder.Entity<ProjectSkill>().HasKey(x => new { x.ProjectsId, x.SkillsId });
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()))
        {
            if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
            {
                property.SetColumnType("decimal(18,2)");
            }
            if (property.Name.EndsWith("Id") || property.Name == "Id")
            {
                property.SetColumnType("nvarchar(450)");
            }
        }

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }
        modelBuilder.Entity<ExpertProfile>().HasOne<ApplicationUser>().WithOne().HasForeignKey<ExpertProfile>(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Wallet>().HasOne<ApplicationUser>().WithOne().HasForeignKey<Wallet>(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<JobPost>().HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Conversation>().HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Conversation>().HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Project>().HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Project>().HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Project>().HasOne(x => x.JobPost).WithMany().HasForeignKey(x => x.JobPostId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Proposal>().HasOne(x => x.Expert).WithMany().HasForeignKey(x => x.ExpertId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Review>().HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Review>().HasOne(x => x.TargetUser).WithMany().HasForeignKey(x => x.TargetUserId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<TransactionLog>().HasOne(x => x.SourceWallet).WithMany().HasForeignKey(x => x.SourceWalletId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<TransactionLog>().HasOne(x => x.DestinationWallet).WithMany().HasForeignKey(x => x.DestinationWalletId).OnDelete(DeleteBehavior.NoAction);
    }
}