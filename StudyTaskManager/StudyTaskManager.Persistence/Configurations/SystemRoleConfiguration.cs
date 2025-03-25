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

            builder
                .Property(sr => sr.Name)
                .HasConversion(
                    t => t.Value,
                    str => Title.Create(str).Value)
                .HasMaxLength(Title.MAX_LENGTH)
                .HasColumnName(TableNames.SystemRoleTable.Name);
        }
    }
}
