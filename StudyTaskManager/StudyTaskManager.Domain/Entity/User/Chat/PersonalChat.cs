using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Entity.User.Chat
{
    /// <summary>
    /// Личный чат между двумя пользователями
    /// </summary>
    public class PersonalChat : BaseEntityWithID
    {
        // Приватный конструктор для создания чата
        private PersonalChat(User User1, User User2)
        {
            this.UserId1 = User1.Id;
            this.User1 = User1;

            this.UserId2 = User2.Id;
            this.User2 = User2;

            _messages = new List<PersonalMessage>();
        }

        /// <summary>
        /// Id пользователя 1
        /// </summary>
        public Guid UserId1 { get; }

        /// <summary>
        /// Id пользователя 2
        /// </summary>
        public Guid UserId2 { get; }

        /// <summary>
        /// Пользователь 1
        /// </summary>
        public User User1 { get; } = null!;

        /// <summary>
        /// Пользователь 2
        /// </summary>
        public User User2 { get; } = null!;

        /// <summary>
        /// Перечисление сообщений из личных чатов
        /// </summary>
        public IReadOnlyCollection<PersonalMessage>? Messages => _messages;
        private List<PersonalMessage>? _messages;

        /// <summary>
        /// Фабричный метод для создания чата
        /// </summary>
        public Result<PersonalChat> Create(PersonalMessage FirstMessage, User Receiver)
        {
            // Можно добавить логику проверки, что два пользователя не могут быть теми же самыми
            if (FirstMessage.Sender.Id == Receiver.Id)
            {
                return Result.Failure<PersonalChat>(DomainErrors.PersonalChat.SameUser);
            }
			var personalChat = new PersonalChat(FirstMessage.Sender, Receiver);

            personalChat.RaiseDomainEvent(new PersonalChatCreatedDomainEvent(personalChat.Id));

			_messages?.Add(FirstMessage);
            return personalChat;
		}
    }
}
