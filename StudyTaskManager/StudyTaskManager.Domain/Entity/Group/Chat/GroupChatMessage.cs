using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.Group.Chat
{
    /// <summary>
    /// Текстовые сообщения оставленные в группе. В отличии от остальных сущностей, оно имеет составной ключ. Он состоит из Id_чата и порядкового номера сообщения
    /// </summary>
    /// Необходимость составного ключа заключается в подгрузке сообщений. Можно выбрать какой-то чат и с легкостью загрузить сообщения с i по n.
    /// Это позволяет реализовать функционал непрочитанных сообщений. Например: если человек прочел последнее сообщение под номером 50. Мы можем подгрузить n сообщений до
    /// и k сообщений после, т.к. они все пронумерованы для определенного чата
    public class GroupChatMessage : BaseEntity
	{
        /// <summary>
        /// Ссылка на id группового чата
        /// </summary>
        public int GroupChatId { get; } 

        /// <summary>
        /// Порядковый номер сообщения
        /// </summary>
        public int Ordinal { get; }

        /// <summary>
        /// Id отправителя сообщения
        /// </summary>
        public int SenderId { get; }

        /// <summary>
        /// Содержание сообщения
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime DateTime { get; }



        /// <summary>
        /// Отправитель сообщения
        /// </summary>
        public User.AbsUser Sender { get; } = null!;

        /// <summary>
        /// Ссылка на групповой чат
        /// </summary>
        public GroupChat GroupChat { get; } = null!;
    }
}
