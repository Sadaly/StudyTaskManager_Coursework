using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
    {
        public void Configure(EntityTypeBuilder<GroupRole> builder)
        {
            builder.ToTable(TableNames.GroupRole);

            builder.HasKey(gr => gr.Id);

            builder
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRoles)
                .HasForeignKey(gr => gr.GroupId)
                .IsRequired(false); // GroupId может быть null

            builder.OwnsOne(gr => gr.Title, title =>
            {
                title
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Title.Create(str).Value.Value)
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName(TableNames.GroupRoleTable.RoleName);
            });
        }
    }
}