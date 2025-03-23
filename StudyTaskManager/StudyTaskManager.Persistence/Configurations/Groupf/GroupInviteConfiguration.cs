using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Groupf
{
    class GroupInviteConfiguration : IEntityTypeConfiguration<GroupInvite>
    {
        public void Configure(EntityTypeBuilder<GroupInvite> builder)
        {
            builder.ToTable(TableNames.GroupInvite);

            builder.HasKey(gi => new { gi.SenderId, gi.ReceiverId, gi.GroupId });

            builder
                .HasOne(gi => gi.Group)
                .WithMany(g => g.GroupInvites)
                .HasForeignKey(gi => gi.GroupId);
            builder
                .HasOne(gi => gi.Sender)
                .WithMany()
                .HasForeignKey(gi => gi.SenderId);
            builder
                .HasOne(gi => gi.Receiver)
                .WithMany()
                .HasForeignKey(gi => gi.ReceiverId);
        }
    }
}
