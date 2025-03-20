using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Persistence.DB.Configurations.Userf
{
    class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
    {
        void IEntityTypeConfiguration<SystemRole>.Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable(TableNames.SystemRole);
            
            // Приватный ключ
            builder.HasKey(sr => sr.Id);

            // Возможно это вообще не нужно
            //// Указываем что юзер может ссылаться на роль
            //builder
            //    .HasMany<User>()
            //    .WithOne()
            //    .HasForeignKey(u => u.SystemRoleId);


            builder
                .Property(sr => sr.Id)
                .ValueGeneratedOnAdd() //автогеренация 
                .IsRequired()
                .HasColumnName(TableNames.UsersTable.Id);
        }
    }
}
