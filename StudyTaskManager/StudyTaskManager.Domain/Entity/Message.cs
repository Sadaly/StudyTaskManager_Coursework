namespace StudyTaskManager.Domain.Entity
{
    class Message
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public User Sender { get; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Content { get; }

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
