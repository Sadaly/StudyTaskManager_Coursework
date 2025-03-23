using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Groupf
{
    class UserInGroupConfiguration : IEntityTypeConfiguration<UserInGroup>
    {
        public void Configure(EntityTypeBuilder<UserInGroup> builder)
        {
            builder.ToTable(TableNames.UserInGroup);

            builder.HasKey(uig => new { uig.UserId, uig.GroupId, uig.RoleId });

            builder
                .HasOne(uig => uig.User)
                .WithMany()
                .HasForeignKey(uig => uig.UserId);
            builder
                .HasOne(uig => uig.Group)
                .WithMany(g => g.UsersInGroup)
                .HasForeignKey(uig => uig.GroupId);
            builder
                .HasOne(uig => uig.Role)
                .WithMany()
                .HasForeignKey(uig => uig.RoleId);
        }
    }
}
