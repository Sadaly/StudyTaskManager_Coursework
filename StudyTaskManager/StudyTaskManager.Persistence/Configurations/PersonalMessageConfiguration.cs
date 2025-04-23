using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTaskManager.Domain.Entity.User.Chat;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Persistence.Configurations
{
    class PersonalMessageConfiguration : IEntityTypeConfiguration<PersonalMessage>
    {
        public void Configure(EntityTypeBuilder<PersonalMessage> builder)
        {
            builder.ToTable(TableNames.PersonalMessage);

            // Приватный ключ
            builder.HasKey(pm => pm.Id);
            // Связь с чатом и отправителем
            builder
                .HasOne(pm => pm.PersonalChat)
                .WithMany(pc => pc.Messages)
                .HasForeignKey(pm => pm.PersonalChatId);
            builder
                .HasOne(pm => pm.Sender)
                .WithMany()
                .HasForeignKey(pm => pm.SenderId);

            builder.OwnsOne(pm => pm.Content, content =>
            {
                content
                    .Property(v => v.Value)
                    .HasConversion(
                        v => v,
                        str => Content.Create(str).Value.Value)
                    .HasMaxLength(Content.MAX_LENGTH)
                    .HasColumnName(TableNames.PersonalMessageTable.Content);
            });
        }
    }
}
