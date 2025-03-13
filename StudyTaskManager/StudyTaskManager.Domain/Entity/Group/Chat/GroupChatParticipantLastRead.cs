using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Показывает последнее прочитанное сообщение пользователем
    /// </summary>
    public class GroupChatParticipantLastRead : BaseEntity
	{
        /// <summary>
        /// Id последнего прочитанного сообщения пользователем в чате. 
        /// Создается лишь тогда, когда пользователь впервые открыл чат. 
        /// Если последнее сообщение в чате совпадает с последним прочитанным, то это поле не меняется
        /// </summary>
        public int LastReadMessageId { get; set; }

        /// <summary>
        /// Id чата, к которому относится прочитанное сообщение
        /// </summary>
        public int GroupChatId { get; }

        /// <summary>
        /// Id пользователя, который посмотрел какое-либо последнее сообщение в чате
        /// </summary>
        public int UserId { get; }



        /// <summary>
        /// Последнее прочитанное сообщение пользователем в чате. 
        /// Создается лишь тогда, когда пользователь впервые открыл чат. 
        /// Если последнее сообщение в чате совпадает с последним прочитанным, то это поле не меняется
        /// </summary>
        public GroupChatMessage ReadMessage { get; set; } = null!;

        /// <summary>
        /// Чат, которому относится прочитанное сообщение
        /// </summary>
        public GroupChat GroupChat { get; } = null!;

        /// <summary>
        /// Пользователь, который посмотрел какое-либо последнее сообщение в чате
        /// </summary>
        public User.AbsUser User { get; } = null!;
    }
}
