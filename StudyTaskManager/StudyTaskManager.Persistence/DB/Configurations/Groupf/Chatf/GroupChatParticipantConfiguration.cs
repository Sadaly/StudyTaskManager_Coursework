using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Persistence.DB.Configurations.Groupf.Chatf
{
    class GroupChatParticipantConfiguration : IEntityTypeConfiguration<GroupChatParticipant>
    {
        public void Configure(EntityTypeBuilder<GroupChatParticipant> builder)
        {
            builder.ToTable(TableNames.GroupChatParticipant);

            builder
                .HasOne(gcp => gcp.GroupChat)
                .WithMany()
                .HasForeignKey(gcp => gcp.GroupChatId);
            builder
                .HasOne(gcp => gcp.User)
                .WithMany()
                .HasForeignKey(gcp => gcp.UserId);
        }
    }
}
