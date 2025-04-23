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

            builder
                .Property(g => g.Title)
                .HasConversion(
                    t => t.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.GroupTable.Title);
            builder
                .Property(g => g.Description)
                .HasConversion(
                    c =>
                        c == null ?
                            null :
                            c.Value,
                    str =>
                        string.IsNullOrEmpty(str) ?
                            null :
                            Content.Create(str).Value)
                .HasMaxLength(Content.MAX_LENGTH)
                .IsRequired(false)
                .HasColumnName(TableNames.GroupTable.Description);
        }
    }
}
