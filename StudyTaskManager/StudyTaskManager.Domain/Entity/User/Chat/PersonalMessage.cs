using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User.Chat
{
    /// <summary>
    /// Личное сообщение от одного пользователя другому
    /// </summary>
    public class PersonalMessage : BaseEntityWithID
    {
        // Приватный конструктор для создания объекта
        private PersonalMessage(User Sender, PersonalChat PersonalChat, Content Content)
        {
            this.Sender = Sender;
            this.SenderId = Sender.Id;

            this.PersonalChat = PersonalChat;
            this.PersonalChatId = PersonalChat.Id;

            this.Content = Content;
            this.DateWriten = DateTime.UtcNow;
            this.Is_Read_By_Other_User = false; // Изначально сообщение считается не прочитанным
        }

        #region свойства

        public Guid SenderId { get; }
        public Guid PersonalChatId { get; }
        public Content Content { get; private set; } = null!;
        public DateTime DateWriten { get; }
        public bool Is_Read_By_Other_User { get; set; }
        public User? Sender { get; } = null!;
        public PersonalChat? PersonalChat { get; } = null!;

        #endregion

        /// <summary>
        /// Фабричный метод для создания личного сообщения
        /// </summary>
        /// <param name="Sender">Отправитель</param>
        /// <param name="PersonalChat">Чат</param>
        /// <param name="Content">Содержимое сообщения</param>
        /// <returns>Новый экземпляр личного сообщения</returns>
        public static PersonalMessage Create(User Sender, PersonalChat PersonalChat, Content Content)
        {
            return new PersonalMessage(Sender, PersonalChat, Content);
        }

        /// <summary>
        /// Метод для обновления флага прочтения сообщения
        /// </summary>
        public void MarkAsRead()
        {
            Is_Read_By_Other_User = true;
        }

        /// <summary>
        /// Метод для изменения содержимого сообщения
        /// </summary>
        /// <param name="newContent">Новое содержание сообщения</param>
        public void UpdateContent(Content newContent)
        {
            this.Content = newContent;
        }
    }
}
