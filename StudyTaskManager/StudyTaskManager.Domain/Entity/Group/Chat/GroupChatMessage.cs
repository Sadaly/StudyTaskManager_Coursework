using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;
using System.Text.Json.Serialization;

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
        protected GroupChatMessage() { } // Для EF Core

        // Приватный конструктор для предотвращения невалидного создания объектов.
        private GroupChatMessage(Guid GroupChatId, ulong ordinal, Guid SenderId, Content content) : base()
        {
            this.GroupChatId = GroupChatId;
            this.Ordinal = ordinal;
            this.SenderId = SenderId;
            this.Content = content;
            this.DateTime = DateTime.UtcNow;
        }

        #region свойства

        /// <summary>
        /// Идентификатор отправителя сообщения.
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// Идентификатор группового чата, в котором оставлено сообщение.
        /// </summary>
        public Guid GroupChatId { get; }

        /// <summary>
        /// Порядковый номер сообщения в чате.
        /// </summary>
        public ulong Ordinal { get; }

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
        [JsonIgnore]
        public User.User? Sender { get; private set; } = null!;

        /// <summary>
        /// Групповой чат, к которому относится сообщение.
        /// </summary>
        [JsonIgnore]
        public GroupChat? GroupChat { get; private set; } = null!;

        #endregion

        /// <summary>
        /// Фабричный метод для создания нового сообщения.
        /// </summary>
        /// <param name="GroupChat">Групповой чат.</param>
        /// <param name="ordinal">Порядковый номер сообщения.</param>
        /// <param name="Sender">Отправитель.</param>
        /// <param name="content">Содержание сообщения.</param>
        /// <returns>Новая сущность GroupChatMessage.</returns>
        public static Result<GroupChatMessage> Create(GroupChat GroupChat, ulong ordinal, User.User Sender, Content content)
        {
            GroupChatMessage gcm = new(GroupChat.Id, ordinal, Sender.Id, content)
            {
                Sender = Sender,
                GroupChat = GroupChat
            };

            gcm.RaiseDomainEvent(new DomainEvents.GroupMessageCreatedDomainEvent(gcm.GroupChatId, gcm.Ordinal));

            return Result.Success(gcm);
        }

        /// <summary>
        /// Метод для изменения содержимого сообщения
        /// </summary>
        /// <param name="newContent">Новое содержание сообщения</param>
        public Result UpdateContent(Content newContent)
        {
            this.Content = newContent;

            RaiseDomainEvent(new DomainEvents.GroupMessageUpdatedDomainEvent(this.GroupChatId, this.Ordinal));

            return Result.Success();
        }
    }
}
