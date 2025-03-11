namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Класс, который показывает принадлежность определенного пользователя к определенному чату
    /// </summary>
    public class GroupChatParticipant
    {
        /// <summary>
        /// Id пользователя, относящийся к чату
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Id чата, к которому пользователь относится
        /// </summary>
        public int GroupChatId { get; }



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
