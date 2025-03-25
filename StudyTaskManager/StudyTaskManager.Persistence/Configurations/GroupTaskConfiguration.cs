using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupTaskConfiguration : IEntityTypeConfiguration<GroupTask>
    {
        public void Configure(EntityTypeBuilder<GroupTask> builder)
        {
            builder.ToTable(TableNames.GroupTask);

            builder.HasKey(gt => gt.Id);

            builder
                .HasOne(gt => gt.Group)
                .WithMany()
                .HasForeignKey(gt => gt.GroupId);
            builder
                .HasOne(gt => gt.Parent)
                .WithMany()
                .HasForeignKey(gt => gt.ParentId);
            builder
                .HasOne(gt => gt.ResponsibleUser)
                .WithMany()
                .HasForeignKey(gt => gt.ResponsibleUserId);
            builder
                .HasOne(gt => gt.Status)
                .WithMany()
                .HasForeignKey(gt => gt.StatusId)
                .IsRequired(false);

            builder
                .Property(gt => gt.HeadLine)
                .HasConversion(
                    t => t.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.GroupTaskTable.HeadLine);
            builder
                .Property(gt => gt.Description)
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
                .HasColumnName(TableNames.GroupTaskTable.Description);
        }
    }
}
