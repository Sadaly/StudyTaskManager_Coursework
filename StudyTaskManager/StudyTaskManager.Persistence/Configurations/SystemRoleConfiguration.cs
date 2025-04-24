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
            builder.HasKey(sr => sr.Id);}
    }
}
