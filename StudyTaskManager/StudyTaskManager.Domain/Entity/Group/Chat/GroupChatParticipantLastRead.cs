using System;
using System.Text.Json.Serialization;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Показывает последнее прочитанное сообщение пользователем в групповом чате.
    /// </summary>
    public class GroupChatParticipantLastRead : BaseEntity
    {
        // Приватный конструктор, доступ к которому можно получить через фабричный метод
        private GroupChatParticipantLastRead(ulong lastReadMessageId, Guid groupChatId, Guid userId) : base()
        {
            LastReadMessageId = lastReadMessageId;
            GroupChatId = groupChatId;
            UserId = userId;
        }

        #region свойства

        /// <summary>
        /// Идентификатор последнего прочитанного сообщения пользователем в чате.
        /// </summary>
        public ulong LastReadMessageId { get; private set; }

        /// <summary>
        /// Идентификатор группового чата, к которому относится прочитанное сообщение.
        /// </summary>
        public Guid GroupChatId { get; }

        /// <summary>
        /// Идентификатор пользователя, который просмотрел последнее сообщение в чате.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Последнее прочитанное сообщение пользователем в чате.
        /// </summary>
        [JsonIgnore]
        public GroupChatMessage? ReadMessage { get; private set; }

        /// <summary>
        /// Групповой чат, к которому относится прочитанное сообщение.
        /// </summary>
        [JsonIgnore]
        public GroupChat? GroupChat { get; private set; }

        /// <summary>
        /// Пользователь, который прочитал последнее сообщение в чате.
        /// </summary>
        [JsonIgnore]
        public User.User? User { get; private set; }

        #endregion

        /// <summary>
        /// Фабричный метод для создания нового отслеживания последнего прочитанного сообщения.
        /// </summary>
        /// <param name="groupChatId">Идентификатор группового чата.</param>
        /// <param name="readMessage">Сообщение, которое было прочитано.</param>
        /// <param name="groupChat">Групповой чат, к которому относится прочитанное сообщение.</param>
        /// <param name="user">Пользователь, который прочитал сообщение.</param>
        /// <returns>Новая сущность GroupChatParticipantLastRead.</returns>
        public static Result<GroupChatParticipantLastRead> Create(User.User user, GroupChat groupChat, GroupChatMessage readMessage)
        {
            var gcp = new GroupChatParticipantLastRead(
                readMessage.Ordinal,
                groupChat.Id,
                user.Id)
            {
                GroupChat = groupChat,
                User = user,
                ReadMessage = readMessage
            };

            gcp.RaiseDomainEvent(new GroupChatParticipantLastReadCreatedDomainEvent(gcp.GroupChatId, gcp.UserId));

            return Result.Success(gcp);
        }

        /// <summary>
        /// Обновление последнего прочитанного сообщения, если оно отличается от текущего.
        /// </summary>
        /// <param name="newReadMessage">Новое прочитанное сообщение.</param>
        public Result UpdateReadMessage(GroupChatMessage newReadMessage)
        {
            if (newReadMessage.Ordinal != LastReadMessageId)
            {
                LastReadMessageId = newReadMessage.Ordinal;
                ReadMessage = newReadMessage;
            }

            RaiseDomainEvent(new GroupChatParticipantLastReadUpdatedDomainEvent(this.GroupChatId, this.UserId));

            return Result.Success();
        }
    }
}
