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
                .WithMany()
                .HasForeignKey(gr => gr.GroupId)
                .IsRequired(false); // GroupId может быть null

            builder
                .Property(g => g.RoleName)
                .HasConversion(
                    t => t.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.GroupRoleTable.RoleName);
        }
    }
}
