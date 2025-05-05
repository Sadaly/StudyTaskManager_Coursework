using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableNames.Group);

            builder.HasKey(g => g.Id);

            builder
                .HasMany(g => g.UsersInGroup)
                .WithOne(uig => uig.Group);
            builder
                .HasOne(g => g.DefaultRole)
                .WithMany()
                .HasForeignKey(g => g.DefaultRoleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder
                .HasMany(g => g.GroupInvites)
                .WithOne(gi => gi.Group);

            // Конфигурация OwnedType для Description
            builder.OwnsOne(g => g.Description);
        }
    }
}
