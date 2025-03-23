using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Persistence.Configurations;

namespace StudyTaskManager.Persistence.Configurations.Userf
{
    class PersonalChatConfiguration : IEntityTypeConfiguration<PersonalChat>
    {
        public void Configure(EntityTypeBuilder<PersonalChat> builder)
        {
            builder.ToTable(TableNames.PersonalChat);

            // Приватный ключ
            builder.HasKey(pc => pc.Id);
            // Внешние ключи на пользователей
            builder
                .HasOne(pc => pc.User1)
                .WithMany(u => u.PersonalChatsAsUser1)
                .HasForeignKey(pc => pc.User1Id);
            builder
                .HasOne(pc => pc.User2)
                .WithMany(u => u.PersonalChatsAsUser2)
                .HasForeignKey(pc => pc.User2Id);
            // Настройка коллекции сообщений
            builder
                .HasMany(pc => pc.Messages)
                .WithOne()
                .HasForeignKey(pm => pm.PersonalChatId);

            // Альтернативные ключи представляют свойства, которые также, как и первичный ключ, должны иметь уникальное значение.
            builder.HasAlternateKey(pc => new { pc.User1Id, pc.User2Id });

            builder.Ignore(pc => pc.Users); // Игнорируем свойство Users

            //// отношение многие ко многим
            //builder
            //    .HasMany(pc => pc.Users)
            //    .WithMany(user => user.PersonalChats);

        }
    }
}
