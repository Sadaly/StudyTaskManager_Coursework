namespace StudyTaskManager.Domain.Entity
{
    /// <summary>
    /// Текстовые сообщения оставленные в группе. В отличии от остальных сущностей, оно имеет составной ключ. Он состоит из Id_чата и порядкового номера сообщения
    /// </summary>
    /// Необходимость составного ключа заключается в подгрузке сообщений. Можно выбрать какой-то чат и с легкостью загрузить сообщения с i по n.
    /// Это позволяет реализовать функционал непрочитанных сообщений. Например: если человек прочел последнее сообщение под номером 50. Мы можем подгрузить n сообщений до
    /// и k сообщений после, т.к. они все пронумерованы для определенного чата
    public class GroupChatMessage
    {
        /// <summary>
        /// Ссылка на id группового чата
        /// </summary>
        public int GroupChatId { get; set; } 
        /// <summary>
        /// Порядковый номер сообщения
        /// </summary>
        public int Ordinal { get; set; }
        /// <summary>
        /// Id автора сообщения
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Содержание сообщения
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Время отправки сообщения
        /// </summary>
        public DateTime DateTime { get; set; }


        /// <summary>
        /// Автор сообщения
        /// </summary>
        public User Author { get; set; } = null!;
        /// <summary>
        /// Ссылка на групповой чат
        /// </summary>
        public GroupChat GroupChat { get; set; } = null!;
    }
}
