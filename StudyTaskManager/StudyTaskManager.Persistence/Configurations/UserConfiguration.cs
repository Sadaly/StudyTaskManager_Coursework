using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
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

            builder.OwnsOne(u => u.Username, username =>
            {
                username
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Username.Create(str).Value.Value)
                    .HasMaxLength(Username.MAX_LENGTH)
                    .HasColumnName(TableNames.UserTable.Username);
            });

            builder.OwnsOne(u => u.Email, email =>
            {
                email
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Email.Create(str).Value.Value)
                    .HasColumnName(TableNames.UserTable.Email);
            });

            builder.OwnsOne(u => u.PhoneNumber, phone =>
            {
                phone
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str =>
                            string.IsNullOrEmpty(str) ?
                                "" :
                                PhoneNumber.Create(str).Value.Value)
                    .HasMaxLength(PhoneNumber.MAX_LENGTH)
                    .IsRequired(false)
                    .HasColumnName(TableNames.UserTable.PhoneNumber);
            });

            builder
                .Property(u => u.PasswordHash)
                .HasConversion(
                    ph => ph.Value,
                    str => new PasswordHash(str))
                .HasColumnName(TableNames.UserTable.PasswordHash);
        }
    }
}