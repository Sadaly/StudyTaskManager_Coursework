using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Persistence.DB.Configurations.Groupf.Taskf
{
    class GroupTaskStatusConfiguration : IEntityTypeConfiguration<GroupTaskStatus>
    {
        public void Configure(EntityTypeBuilder<GroupTaskStatus> builder)
        {
            builder.ToTable(TableNames.GroupTaskStatus);

            builder.HasKey(gts => gts.Id);

            builder
                .HasOne(gts => gts.Group)
                .WithMany()
                .HasForeignKey(gts => gts.GroupId);
        }
    }
}
