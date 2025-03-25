using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Userf
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.User);

            // Приватный ключ
            builder.HasKey(user => user.Id);
            // Внешний ключ для роли
            builder
                .HasOne(user => user.SystemRole)
                .WithMany()
                .HasForeignKey(user => user.SystemRoleId);

            builder.Ignore(user => user.PersonalChats);

            builder
                .Property(u => u.UserName)
                .HasConversion(
                    un => un.Value,
                    str => UserName.Create(str).Value)
                .HasMaxLength(UserName.MAX_LENGTH)
                .HasColumnName(TableNames.UserTable.UserName);
            builder
                .Property(u => u.Email)
                .HasConversion(
                    e => e.Value,
                    str => Email.Create(str).Value)
                .HasColumnName(TableNames.UserTable.Email);
            builder
                .Property(u => u.PhoneNumber)
                .HasConversion(
                    pn =>
                        pn == null ?
                            null :
                            pn.Value,
                    str =>
                        string.IsNullOrEmpty(str) ?
                            null :
                            PhoneNumber.Create(str).Value)
                .HasMaxLength(PhoneNumber.MAX_LENGTH)
                // проверки на мин длинну нет
                .IsRequired(false)
                .HasColumnName(TableNames.UserTable.PhoneNumber);
            builder
                .Property(u => u.PasswordHash)
                .HasConversion(
                    ph => ph.Value,
                    str => new PasswordHash(str))
                .HasColumnName(TableNames.UserTable.PasswordHash);
        }
    }
}