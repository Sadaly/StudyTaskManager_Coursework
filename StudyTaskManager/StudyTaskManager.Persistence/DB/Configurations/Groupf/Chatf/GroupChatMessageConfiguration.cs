using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.DB.Configurations.Groupf.Chatf
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
                .WithMany()
                .HasForeignKey(gcm => gcm.GroupChatId);
        }
    }
}
