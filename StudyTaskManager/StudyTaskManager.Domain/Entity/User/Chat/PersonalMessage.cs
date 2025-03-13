using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity.User.Chat
{
    /// <summary>
    /// Личное сообщение от одного пользователя другому
    /// </summary>
    public class PersonalMessage : BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сообщения
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Id отправителя
        /// </summary>
        public int SenderId { get; }

        /// <summary>
        /// Id личного чата
        /// </summary>
        public int PersonalChatId { get; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Content { get; } = null!;

        /// <summary>
        /// Дата написания
        /// </summary>
        public DateTime DateWriten { get; }

        /// <summary>
        /// Флаг прочитано собеседником 
        /// </summary>
        public bool Is_Read_By_Other_User { get; set; }



        /// <summary>
        /// Отправитель
        /// </summary>
        public AbsUser Sender { get; } = null!;

        /// <summary>
        /// Личный чат
        /// </summary>
        public PersonalChat PersonalChat { get; } = null!;
    }
}
