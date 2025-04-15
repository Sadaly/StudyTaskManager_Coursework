using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace StudyTaskManager.Domain.Entity.User.Chat
{
    /// <summary>
    /// Личный чат между двумя пользователями
    /// </summary>
    public class PersonalChat : BaseEntityWithID
    {
        // Приватный конструктор для создания чата
        private PersonalChat(Guid id, Guid user1Id, Guid user2Id) : base(id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
            _messages = [];
        }

        #region свойства

        public Guid User1Id { get; }
        public Guid User2Id { get; }
        [JsonIgnore]
        public IEnumerable<Guid> UsersID
        {
            get
            {
                yield return User1Id;
                yield return User2Id;
            }
        }
        [JsonIgnore]
        public User? User1 { get; private set; } = null!;
        [JsonIgnore]
        public User? User2 { get; private set; } = null!;
        //public IEnumerable<User> Users
        //{
        //    get
        //    {
        //        if (User1 != null) yield return User1;
        //        if (User2 != null) yield return User2;
        //    }
        //}

        /// <summary>
        /// Перечисление сообщений из личных чатов
        /// </summary>
        public List<PersonalMessage>? Messages => _messages;
        readonly List<PersonalMessage>? _messages;

        #endregion

        /// <summary>
        /// Фабричный метод для создания чата
        /// </summary>
        public static Result<PersonalChat> Create(User User1, User User2)
        {
            // Можно добавить логику проверки, что два пользователя не могут быть теми же самыми
            if (User1.Id == User2.Id)
            {
                return Result.Failure<PersonalChat>(PersistenceErrors.PersonalChat.SameUser);
            }
            PersonalChat pc = new(Guid.Empty, User1.Id, User2.Id)
            {
                User1 = User1,
                User2 = User2
            };

            pc.RaiseDomainEvent(new DomainEvents.PersonalChatCreatedDomainEvent(pc.Id));

            return Result.Success(pc);
        }


        /// <summary>
        /// Метод для добавления сообщения в чат
        /// </summary>
        /// <param name="message">Сообщение</param>
        public Result AddMessage(PersonalMessage message)
        {
            _messages?.Add(message);

            RaiseDomainEvent(new PersonalChatMessageAddedDomainEvent(message.Id));

            return Result.Success();
        }
    }
}
