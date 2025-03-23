using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Groupf.Chatf
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

            builder
                .Property(gc => gc.Name)
                .HasConversion(
                    n => n.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.GroupChatTable.Title);
        }
    }
}
