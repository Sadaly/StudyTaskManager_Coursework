using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Persistence.DB.Configurations.Userf
{
    class PersonalChatConfiguration : IEntityTypeConfiguration<PersonalChat>
    {
        public void Configure(EntityTypeBuilder<PersonalChat> builder)
        {
            builder.ToTable(TableNames.PersonalChat);

            // Приватный ключ
            builder.HasKey(pc => pc.Id);
            //// Внешние ключи на пользователей
            //builder
            //    .HasOne(pc => pc.User1)
            //    .WithMany(u => u.PersonalChatsAsUser1)
            //    .HasForeignKey(pc => pc.UserId1);
            //builder
            //    .HasOne(pc => pc.User2)
            //    .WithMany(u => u.PersonalChatsAsUser2)
            //    .HasForeignKey(pc => pc.UserId2);

            // отношение многие ко многим
            builder
                .HasMany(pc => pc.Users)
                .WithMany(user => user.PersonalChats);

            //// Уникальный индекс на основе двух пользователей
            //builder.HasIndex(pc => new { pc.UserId1, pc.UserId2 }).IsUnique();
        }
    }
}
