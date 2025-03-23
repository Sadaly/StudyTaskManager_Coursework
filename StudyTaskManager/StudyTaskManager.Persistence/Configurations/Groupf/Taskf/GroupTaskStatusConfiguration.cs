using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations.Groupf.Taskf
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

            builder
                .Property(gts => gts.Name)
                .HasConversion(
                    t => t.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.GroupTaskStatusTable.Name);
            builder
                .Property(gts => gts.Description)
                .HasConversion(
                    c =>
                        c == null ?
                            null :
                            c.Value,
                    str =>
                        string.IsNullOrEmpty(str) ?
                            null :
                            Content.Create(str).Value)
                .HasMaxLength(Content.MAX_LENGTH)
                .IsRequired(false)
                .HasColumnName(TableNames.GroupTaskStatusTable.Description);
        }
    }
}
