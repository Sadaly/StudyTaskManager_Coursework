using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
    {
        public void Configure(EntityTypeBuilder<GroupChat> builder)
        {
            builder.ToTable(TableNames.GroupChat);

            builder.HasKey(gc => gc.Id);

            builder
                .HasOne(gc => gc.Group)
                .WithMany()
                .HasForeignKey(gc => gc.GroupId);

            builder.OwnsOne(gc => gc.Name, name =>
            {
                name
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Title.Create(str).Value.Value)
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName(TableNames.GroupChatTable.Title);
            });
        }
    }
}
