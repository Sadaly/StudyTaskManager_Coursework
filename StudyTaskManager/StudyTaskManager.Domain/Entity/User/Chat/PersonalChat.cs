using StudyTaskManager.Domain.Common;
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
        private PersonalChat(User user1, User user2)
        {
            UserId1 = user1.Id;
            User1 = user1;

            UserId2 = user2.Id;
            User2 = user2;

            _messages = [];
        }

        public Guid UserId1 { get; }
        public Guid UserId2 { get; }
        public IEnumerable<Guid> UsersID
        {
            get
            {
                yield return UserId1;
                yield return UserId2;
            }
        }
        public User User1 { get; } = null!;
        public User User2 { get; } = null!;
        public IEnumerable<User> Users
        {
            get
            {
                yield return User1;
                yield return User2;
            }
        }

        readonly List<PersonalMessage>? _messages;
        public IReadOnlyCollection<PersonalMessage>? Messages => _messages;

        /// <summary>
        /// Фабричный метод для создания чата
        /// </summary>
        public Result<PersonalChat> Create(User User1, User User2)
        {
            // Можно добавить логику проверки, что два пользователя не могут быть теми же самыми
            if (User1.Id == User2.Id)
            {
                return Result.Failure<PersonalChat>(DomainErrors.PersonalChat.SameUser);
            }

            return new PersonalChat(User1, User2);
        }

        /// <summary>
        /// Метод для добавления сообщения в чат
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void AddMessage(PersonalMessage message)
        {
            _messages?.Add(message);
        }
    }
}
