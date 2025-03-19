using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Текстовые сообщения, оставленные в групповом чате. Имеет составной ключ из GroupChatId и Ordinal.
    /// </summary>
    /// <remarks>
    /// Составной ключ позволяет эффективно подгружать сообщения, а также реализовать функционал непрочитанных сообщений.
    /// Например, можно легко подгрузить сообщения от i до n, с возможностью отслеживания прочитанных сообщений.
    /// </remarks>
    public class GroupChatMessage : BaseEntity
    {
        // Приватный конструктор для предотвращения невалидного создания объектов.
        private GroupChatMessage(GroupChat GroupChat, ulong ordinal, User.User Sender, Content content)
        {
            this.GroupChatId = GroupChat.Id;
            this.GroupChat = GroupChat;

            this.Ordinal = ordinal;
            this.SenderId = Sender.Id;
            this.Sender = Sender;

            this.Content = content;
            this.DateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Идентификатор группового чата, в котором оставлено сообщение.
        /// </summary>
        public Guid GroupChatId { get; }

        /// <summary>
        /// Порядковый номер сообщения в чате.
        /// </summary>
        public ulong Ordinal { get; }

        /// <summary>
        /// Идентификатор отправителя сообщения.
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// Содержание сообщения.
        /// </summary>
        public Content Content { get; set; } = null!;

        /// <summary>
        /// Время отправки сообщения.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Отправитель сообщения.
        /// </summary>
        public User.User Sender { get; } = null!;

        /// <summary>
        /// Групповой чат, к которому относится сообщение.
        /// </summary>
        public GroupChat GroupChat { get; } = null!;

        /// <summary>
        /// Фабричный метод для создания нового сообщения.
        /// </summary>
        /// <param name="GroupChat">Групповой чат.</param>
        /// <param name="ordinal">Порядковый номер сообщения.</param>
        /// <param name="Sender">Отправитель.</param>
        /// <param name="content">Содержание сообщения.</param>
        /// <returns>Новая сущность GroupChatMessage.</returns>
        public static GroupChatMessage Create(GroupChat GroupChat, ulong ordinal, User.User Sender, Content content)
        {
            return new GroupChatMessage(GroupChat, ordinal, Sender, content);
        }
    }
}
