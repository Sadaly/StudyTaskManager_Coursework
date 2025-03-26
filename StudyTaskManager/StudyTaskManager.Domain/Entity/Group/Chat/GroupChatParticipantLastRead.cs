using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Показывает последнее прочитанное сообщение пользователем в групповом чате.
    /// </summary>
    public class GroupChatParticipantLastRead : BaseEntity
    {
        // Приватный конструктор, доступ к которому можно получить через фабричный метод
        private GroupChatParticipantLastRead(ulong lastReadMessageId, Guid groupChatId, Guid userId)
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
        public GroupChatMessage? ReadMessage { get; private set; }

        /// <summary>
        /// Групповой чат, к которому относится прочитанное сообщение.
        /// </summary>
        public GroupChat? GroupChat { get; private set; }

        /// <summary>
        /// Пользователь, который прочитал последнее сообщение в чате.
        /// </summary>
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
        public static GroupChatParticipantLastRead Create(Guid groupChatId, GroupChatMessage readMessage, GroupChat groupChat, User.User user)
        {
            return
                new GroupChatParticipantLastRead(
                    readMessage.Ordinal,
                    groupChatId,
                    user.Id
                )
                {
                    GroupChat = groupChat,
                    User = user,
                    ReadMessage = readMessage
                };
        }

        /// <summary>
        /// Обновление последнего прочитанного сообщения, если оно отличается от текущего.
        /// </summary>
        /// <param name="newReadMessage">Новое прочитанное сообщение.</param>
        public void UpdateReadMessage(GroupChatMessage newReadMessage)
		{
			//Todo добавить событие
			if (newReadMessage.Ordinal != LastReadMessageId)
            {
                LastReadMessageId = newReadMessage.Ordinal;
                ReadMessage = newReadMessage;
            }
        }
    }
}
