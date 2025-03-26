using Microsoft.EntityFrameworkCore;

namespace StudyTaskManager.Persistence
{
    public class AppDbContext : DbContext
    {
        #region DbSet
        public DbSet<Domain.Entity.User.User> Users { get; set; }
        public DbSet<Domain.Entity.User.SystemRole> SystemRoles { get; set; }
        public DbSet<Domain.Entity.User.BlockedUserInfo> DlockedUserInfos { get; set; }

        public DbSet<Domain.Entity.User.Chat.PersonalChat> PersonalChats { get; set; }
        public DbSet<Domain.Entity.User.Chat.PersonalMessage> PersonalMessages { get; set; }

        public DbSet<Domain.Entity.Group.Group> Groups { get; set; }
        public DbSet<Domain.Entity.Group.GroupInvite> GroupInvites { get; set; }
        public DbSet<Domain.Entity.Group.GroupRole> GroupRoles { get; set; }
        public DbSet<Domain.Entity.Group.UserInGroup> UserInGroups { get; set; }

        public DbSet<Domain.Entity.Group.Chat.GroupChat> GroupChats { get; set; }
        public DbSet<Domain.Entity.Group.Chat.GroupChatMessage> GroupChatMessages { get; set; }
        public DbSet<Domain.Entity.Group.Chat.GroupChatParticipant> GroupChatParticipants { get; set; }
        public DbSet<Domain.Entity.Group.Chat.GroupChatParticipantLastRead> GetGroupChatParticipantLastReads { get; set; }

        public DbSet<Domain.Entity.Group.Task.GroupTask> GroupTasks { get; set; }
        public DbSet<Domain.Entity.Group.Task.GroupTaskStatus> GroupTaskStatuses { get; set; }
        public DbSet<Domain.Entity.Group.Task.GroupTaskUpdate> GroupTaskUpdates { get; set; }
        #endregion
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region modelBuilder.ApplyConfiguration
            modelBuilder.ApplyConfiguration(new Configurations.BlockedUserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonalChatConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonalMessageConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SystemRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupChatConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupChatParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupChatParticipantLastReadConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupTaskConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupTaskStatusConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupTaskUpdateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupInviteConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.GroupRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserInGroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OutboxMessageConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Username=postgres;Password=password;Host=localhost;Port=5432;Database=dbtest;");
            //base.OnConfiguring(optionsBuilder);
        }
    }
}
