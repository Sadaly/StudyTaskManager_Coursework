using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Groupf.Taskf
{
    class GroupTaskUpdateConfiguration : IEntityTypeConfiguration<GroupTaskUpdate>
    {
        public void Configure(EntityTypeBuilder<GroupTaskUpdate> builder)
        {
            builder.ToTable(TableNames.GroupTaskStatus);

            builder.HasKey(gts => gts.Id);

            builder
                .HasOne(gts => gts.Task)
                .WithMany()
                .HasForeignKey(gts => gts.TaskId);
            builder
                .HasOne(gts => gts.Creator)
                .WithMany()
                .HasForeignKey(gts => gts.CreatorId);
        }
    }
}
