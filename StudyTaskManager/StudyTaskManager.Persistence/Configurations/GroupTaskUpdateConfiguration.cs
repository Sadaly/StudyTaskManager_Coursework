using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupTaskUpdateConfiguration : IEntityTypeConfiguration<GroupTaskUpdate>
    {
        public void Configure(EntityTypeBuilder<GroupTaskUpdate> builder)
        {
            builder.ToTable(TableNames.GroupTaskUpdate);

            builder.HasKey(gtu => gtu.Id);

            builder
                .HasOne(gtu => gtu.Task)
                .WithMany()
                .HasForeignKey(gtu => gtu.TaskId);
            builder
                .HasOne(gtu => gtu.Creator)
                .WithMany()
                .HasForeignKey(gtu => gtu.CreatorId);

            builder
                .Property(gtu => gtu.Content)
                .HasConversion(
                    c => c.Value,
                    str => Content.Create(str).Value)
                .HasMaxLength(Content.MAX_LENGTH)
                .HasColumnName(TableNames.GroupTaskUpdateTable.Content);
        }
    }
}
