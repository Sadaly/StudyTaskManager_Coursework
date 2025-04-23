using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupChatMessageConfiguration : IEntityTypeConfiguration<GroupChatMessage>
    {
        public void Configure(EntityTypeBuilder<GroupChatMessage> builder)
        {
            builder.ToTable(TableNames.GroupChatMessage);

            builder.HasKey(gcm => new { gcm.GroupChatId, gcm.Ordinal });

            builder
                .HasOne(gcm => gcm.Sender)
                .WithMany()
                .HasForeignKey(gcm => gcm.SenderId);
            builder
                .HasOne(gcm => gcm.GroupChat)
                .WithMany(gc => gc.GroupChatMessages)
                .HasForeignKey(gcm => gcm.GroupChatId);

            builder.OwnsOne(gcm => gcm.Content, content =>
            {
                content
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Content.Create(str).Value.Value)
                    .HasMaxLength(Content.MAX_LENGTH)
                    .HasColumnName(TableNames.GroupChatMessageTable.Context);
            });
        }
    }
}
