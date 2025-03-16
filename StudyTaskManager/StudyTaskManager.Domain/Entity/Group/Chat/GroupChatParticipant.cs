using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Класс, который показывает принадлежность определенного пользователя к определенному чату
    /// </summary>
    public class GroupChatParticipant : BaseEntity
	{
        /// <summary>
        /// Id пользователя, относящийся к чату
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Id чата, к которому пользователь относится
        /// </summary>
        public Guid GroupChatId { get; }



        /// <summary>
        /// Пользователь, относящийся к чату
        /// </summary>
        public User.AbsUser User { get; } = null!;

        /// <summary>
        /// Чат, к которому пользователь относится
        /// </summary>
        public GroupChat GroupChat { get; } = null!;
    }
}
