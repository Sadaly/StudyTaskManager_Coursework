using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Класс, который показывает принадлежность определенного пользователя к определенному чату.
    /// </summary>
    public class GroupChatParticipant : BaseEntity
    {
        // Приватный конструктор, доступ к которому можно получить через фабричный метод.
        private GroupChatParticipant(User.User user, GroupChat groupChat)
        {
            UserId = user.Id;
            GroupChatId = groupChat.Id;
            User = user;
            GroupChat = groupChat;
        }

        /// <summary>
        /// Id пользователя, относящийся к чату.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Id чата, к которому пользователь относится.
        /// </summary>
        public Guid GroupChatId { get; }

        /// <summary>
        /// Пользователь, относящийся к чату.
        /// </summary>
        public User.User User { get; }

        /// <summary>
        /// Чат, к которому пользователь относится.
        /// </summary>
        public GroupChat GroupChat { get; }

        /// <summary>
        /// Фабричный метод для создания нового участника чата.
        /// </summary>
        /// <param name="user">Пользователь, относящийся к чату.</param>
        /// <param name="groupChat">Групповой чат, к которому относится пользователь.</param>
        /// <returns>Новая сущность GroupChatParticipant.</returns>
        public static GroupChatParticipant Create(User.User user, GroupChat groupChat)
        {
            return new GroupChatParticipant(user, groupChat);
        }
    }
}
