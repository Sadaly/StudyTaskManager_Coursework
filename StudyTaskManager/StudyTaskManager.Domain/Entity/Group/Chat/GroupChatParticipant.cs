using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Класс, который показывает принадлежность определенного пользователя к определенному чату.
    /// </summary>
    public class GroupChatParticipant : BaseEntity
    {
        // Приватный конструктор, доступ к которому можно получить через фабричный метод.
        private GroupChatParticipant(Guid userId, Guid groupChatId) : base()
        {
            UserId = userId;
            GroupChatId = groupChatId;
        }

        #region свойства

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
        public User.User? User { get; private set; }

        /// <summary>
        /// Чат, к которому пользователь относится.
        /// </summary>
        public GroupChat? GroupChat { get; private set; }

        #endregion

        /// <summary>
        /// Фабричный метод для создания нового участника чата.
        /// </summary>
        /// <param name="user">Пользователь, относящийся к чату.</param>
        /// <param name="groupChat">Групповой чат, к которому относится пользователь.</param>
        /// <returns>Новая сущность GroupChatParticipant.</returns>
        public static Result<GroupChatParticipant> Create(User.User user, GroupChat groupChat)
        {
            GroupChatParticipant gcp = new(user.Id, groupChat.Id)
            {
                User = user,
                GroupChat = groupChat
            };

            gcp.RaiseDomainEvent(new GroupChatParticipantCreatedDomainEvent(groupChat.Id, user.Id));

            return Result.Success(gcp);
        }
    }
}
