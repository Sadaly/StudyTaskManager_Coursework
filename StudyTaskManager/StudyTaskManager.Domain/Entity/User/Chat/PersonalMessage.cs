using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.Shared;
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

        /// <summary>
        /// Id отправителя
        /// </summary>
        public Guid SenderId { get; }

        /// <summary>
        /// Id личного чата
        /// </summary>
        public Guid PersonalChatId { get; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public Content Content { get; private set; } = null!;

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
        public User? Sender { get; private set; } = null!;

        /// <summary>
        /// Личный чат
        /// </summary>
        public PersonalChat? PersonalChat { get; private set; } = null!;

        #endregion

        /// <summary>
        /// Фабричный метод для создания личного сообщения
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="personalChat">Чат</param>
        /// <param name="content">Содержимое сообщения</param>
        /// <returns>Новый экземпляр личного сообщения</returns>
        public static Result<PersonalMessage> Create(User sender, PersonalChat personalChat, Content content)
        {
            var pm = new PersonalMessage(Guid.Empty, sender.Id, personalChat.Id, content)
            {
                Sender = sender,
                PersonalChat = personalChat
            };

            pm.RaiseDomainEvent(new DomainEvents.PersonalMessageCreatedDomainEvent(pm.Id));

            return Result.Success(pm);
        }

        /// <summary>
        /// Метод для обновления флага прочтения сообщения
        /// </summary>
        public Result MarkAsRead()
        {
            Is_Read_By_Other_User = true;

            RaiseDomainEvent(new DomainEvents.PersonalMessageReadDomainEvent(this.Id));

            return Result.Success();
        }

        /// <summary>
        /// Метод для изменения содержимого сообщения
        /// </summary>
        /// <param name="newContent">Новое содержание сообщения</param>
        public Result UpdateContent(Content newContent)
        {
            this.Content = newContent;

            RaiseDomainEvent(new DomainEvents.PersonalMessageUpdatedDomainEvent(this.Id));

            return Result.Success();
        }
    }
}
