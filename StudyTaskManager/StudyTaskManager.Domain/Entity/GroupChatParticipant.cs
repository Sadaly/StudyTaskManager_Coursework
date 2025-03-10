namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Класс, который показывает принадлежность определенного пользователя к определенному чату
    /// </summary>
    public class GroupChatParticipant
    {
        /// <summary>
        /// Id пользователя, относящийся к чату
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id чата, к которому пользователь относится
        /// </summary>
        public int GroupChatId { get; set; }


        /// <summary>
        /// Пользователь, относящийся к чату
        /// </summary>
        public User User { get; set; } = null!;
        /// <summary>
        /// Чат, к которому пользователь относится
        /// </summary>
        public GroupChat GroupChat { get; set; } = null!;
    }
}
