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
        private PersonalMessage(Guid id, Guid senderId, Guid personalChatId) : base(id)
        {
            SenderId = senderId;
            PersonalChatId = personalChatId;
            DateWriten = DateTime.UtcNow;
            Is_Read_By_Other_User = false; // Изначально сообщение считается не прочитанным
        }
        private PersonalMessage(Guid id, Guid senderId, Guid personalChatId, Content content) : this(id, senderId, personalChatId)
        {
            Content = content;
        }

        #region свойства

        public Guid SenderId { get; }
        public Guid PersonalChatId { get; }
        public Content Content { get; private set; } = null!;
        public DateTime DateWriten { get; }
        public bool Is_Read_By_Other_User { get; set; }
        public User? Sender { get; private set; } = null!;
        public PersonalChat? PersonalChat { get; private set; } = null!;

        #endregion

        /// <summary>
        /// Фабричный метод для создания личного сообщения
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="personalChat">Чат</param>
        /// <param name="content">Содержимое сообщения</param>
        /// <returns>Новый экземпляр личного сообщения</returns>
        public static PersonalMessage Create(User sender, PersonalChat personalChat, Content content)
        {
            return new PersonalMessage(Guid.Empty, sender.Id, personalChat.Id, content)
            {
                Sender = sender,
                PersonalChat = personalChat
            };
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
