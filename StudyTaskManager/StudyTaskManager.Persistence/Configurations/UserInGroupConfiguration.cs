using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.Configurations
{
    class UserInGroupConfiguration : IEntityTypeConfiguration<UserInGroup>
    {
        public void Configure(EntityTypeBuilder<UserInGroup> builder)
        {
            builder.ToTable(TableNames.UserInGroup);

            builder.HasKey(uig => new { uig.UserId, uig.GroupId});

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
