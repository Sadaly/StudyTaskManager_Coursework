using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.DB.Configurations.Groupf
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
                .HasForeignKey(gr => gr.GroupId);
        }
    }
}
