using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
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
