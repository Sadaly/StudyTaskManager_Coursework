using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Persistence.Configurations;
using System.Numerics;

namespace StudyTaskManager.Persistence.Configurations.Groupf.Chatf
{
    class GroupChatParticipantLastReadConfiguration : IEntityTypeConfiguration<GroupChatParticipantLastRead>
    {
        public void Configure(EntityTypeBuilder<GroupChatParticipantLastRead> builder)
        {
            builder.ToTable(TableNames.GroupChatParticipantLastRead);

            builder
                .HasOne(gcplr => gcplr.GroupChat)
                .WithMany()
                .HasForeignKey(gcplr => gcplr.GroupChatId);
            builder
                .HasOne(gcplr => gcplr.User)
                .WithMany()
                .HasForeignKey(gcplr => gcplr.UserId);

            builder
                .HasOne(gcplr => gcplr.ReadMessage)
                .WithMany()
                .HasForeignKey(gcplr => new { gcplr.GroupChatId, gcplr.LastReadMessageId });

        }
    }
}
