using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.DB.Configurations.Groupf
{
    class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableNames.Group);

            builder.HasKey(g => g.Id);

            builder
                .HasOne(g => g.DefaultRole)
                .WithMany()
                .HasForeignKey(g => g.DefaultRoleId);
            builder
                .HasMany(g => g.UsersInGroup)
                .WithMany();
            builder
                .HasMany(g => g.GroupRoles)
                .WithMany();
            builder
                .HasMany(g => g.GroupInvites)
                .WithMany();
        }
    }
}
