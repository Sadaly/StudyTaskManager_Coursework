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

            builder.OwnsOne(g => g.Title, title =>
            {
                title
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Title.Create(str).Value.Value)
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName(TableNames.GroupTable.Title);
            });

            builder.OwnsOne(g => g.Description, description =>
            {
                description
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str =>
                            string.IsNullOrEmpty(str) ?
                                "" :
                                Content.Create(str).Value.Value)
                    .HasMaxLength(Content.MAX_LENGTH)
                    .IsRequired(false)
                    .HasColumnName(TableNames.GroupTable.Description);
            });
        }
    }
}
