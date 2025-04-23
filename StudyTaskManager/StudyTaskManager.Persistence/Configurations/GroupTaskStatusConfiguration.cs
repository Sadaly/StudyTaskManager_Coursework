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

            builder.OwnsOne(gts => gts.Name, name =>
            {
                name
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Title.Create(str).Value.Value)
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName(TableNames.GroupTaskStatusTable.Name);
            });

            builder.OwnsOne(gts => gts.Description, description =>
            {
                description
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str =>
                            string.IsNullOrEmpty(str) ?
                                "" :
                                Content.Create(str).Value.Value)
                    .HasMaxLength(Content.MAX_LENGTH)
                    .IsRequired(false)
                    .HasColumnName(TableNames.GroupTaskStatusTable.Description);
            });
        }
    }
}
