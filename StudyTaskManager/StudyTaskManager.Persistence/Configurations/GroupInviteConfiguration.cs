using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupInviteConfiguration : IEntityTypeConfiguration<GroupInvite>
    {
        public void Configure(EntityTypeBuilder<GroupInvite> builder)
        {
            builder.ToTable(TableNames.GroupInvite);

            builder.HasKey(gi => new { gi.ReceiverId, gi.GroupId });

            builder
                .HasOne(gi => gi.Group)
                .WithMany(g => g.GroupInvites)
                .HasForeignKey(gi => gi.GroupId);
            builder
                .HasOne(gi => gi.Sender)
                .WithMany()
                .HasForeignKey(gi => gi.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasOne(gi => gi.Receiver)
                .WithMany()
                .HasForeignKey(gi => gi.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
