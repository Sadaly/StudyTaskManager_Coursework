using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
    {
        void IEntityTypeConfiguration<SystemRole>.Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable(TableNames.SystemRole);

            // Приватный ключ
            builder.HasKey(sr => sr.Id);

            builder.OwnsOne(sr => sr.Name, name =>
            {
                name
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Title.Create(str).Value.Value)
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName(TableNames.SystemRoleTable.Name);
            });
        }
    }
}
