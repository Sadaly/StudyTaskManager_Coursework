using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Persistence.Constants;

namespace StudyTaskManager.Persistence.Configurations
{
    internal sealed class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder
                .Property(x => x.UserName)
                .HasConversion(v => v.Value, v => UserName.Create(v).Value)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .HasConversion(v => v.Value, v => Email.Create(v).Value)
                .IsRequired();

            builder
                .Property(x => x.PasswordHash)
                .HasConversion(
                    v => v.Value, 
                    v => PasswordHash.Create(Password.Create(v).Value).Value)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .HasConversion(
                    v => v != null ? v.Value : null, 
                    v => v != null ? PhoneNumber.Create(v).Value : null)
                .IsRequired(false);

            builder.Property(x => x.SystemRoleId)
                .IsRequired(false);

            builder.Property(x => x.RegistrationDate)
                .IsRequired();

            builder.HasOne(x => x.SystemRole)
                .WithMany()
                .HasForeignKey(x => x.SystemRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PersonalChatsAsUser1)
                .WithOne()
                .HasForeignKey(x => x.UserId1)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PersonalChatsAsUser2)
                .WithOne()
                .HasForeignKey(x => x.UserId2)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
