namespace StudyTaskManager.Domain.Entity
{
    public class PersonalMessage
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public User Sender { get; } = null!;

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Content { get; } = null!;

        /// <summary>
        /// Дата написания
        /// </summary>
        public DateTime DateWriting { get; }

        /// <summary>
        /// Флаг прочитано собеседником 
        /// </summary>
        public bool FlagReadByTheInterviewee { get; }
    }
}
