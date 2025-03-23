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


            //builder
            //    .Property(user => user.Id)
            //    .ValueGeneratedOnAdd() //автогеренация 
            //    .IsRequired()
            //    .HasColumnName(TableNames.UsersTable.Id);
            //builder
            //    .Property(user => user.SystemRoleId)
            //    .IsRequired(false)
            //    .HasColumnName(TableNames.UsersTable.SystemRoleId);
            //builder
            //    .HasOne(u => u.SystemRoleId)    // только один юзера может быть только один
            //    .WithMany()         // у чегото может быть много юзеров
            //    .HasForeignKey();   // указывает на внешний ключ
        }
    }
}
//namespace StudyTaskManager.Persistence.temporarily.Configurations
//{
//    internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
//    {
//        public void Configure(EntityTypeBuilder<User> builder)
//        {
//            builder.ToTable(TableNames.Users);

//            builder.HasKey(x => x.Id);
//            builder.Property(x => x.Id).IsRequired();

//            builder
//                .Property(x => x.UserName)
//                .HasConversion(v => v.Value, v => UserName.Create(v).Value)
//                .IsRequired();

//            builder
//                .Property(x => x.Email)
//                .HasConversion(v => v.Value, v => Email.Create(v).Value)
//                .IsRequired();

//            builder
//                .Property(x => x.PasswordHash)
//                .HasConversion(
//                    v => v.Value,
//                    v => PasswordHash.Create(Password.Create(v).Value).Value)
//                .IsRequired();

//            builder
//                .Property(x => x.PhoneNumber)
//                .HasConversion(
//                    v => v != null ? v.Value : null,
//                    v => v != null ? PhoneNumber.Create(v).Value : null)
//                .IsRequired(false);

//            builder.Property(x => x.SystemRoleId)
//                .IsRequired(false);

//            builder.Property(x => x.RegistrationDate)
//                .IsRequired();

//            builder.HasOne(x => x.SystemRole)
//                .WithMany()
//                .HasForeignKey(x => x.SystemRoleId)
//                .OnDelete(DeleteBehavior.Restrict);

//            builder.HasMany(x => x.PersonalChatsAsUser1)
//                .WithOne()
//                .HasForeignKey(x => x.UserId1)
//                .OnDelete(DeleteBehavior.Restrict);

//            builder.HasMany(x => x.PersonalChatsAsUser2)
//                .WithOne()
//                .HasForeignKey(x => x.UserId2)
//                .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}
