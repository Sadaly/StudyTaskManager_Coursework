using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User : BaseEntityWithID
    {
        /// <summary>
        /// Конструктор класс <see cref="User"/>
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="phoneNumber">Номер телефона, можно оставить null</param>
        /// <param name="systemRole">Роль, можно оставить null</param>
        private User(Guid id, UserName userName, Email email, Password password, PhoneNumber? phoneNumber, SystemRole? systemRole) : base(id)
        {
            UserName = userName;
            Email = email;
            PasswordHash = PasswordHash.Create(password).Value;

            if (phoneNumber != null)
            {
                PhoneNumber = phoneNumber;
            }

            if (systemRole != null)
            {
                SystemRoleId = systemRole.Id;
                SystemRole = systemRole;
            }

            this.RegistrationDate = DateTime.UtcNow;

            // Инициализация списка личных чатов
            _personalChatsAsUser1 = [];
            _personalChatsAsUser2 = [];
        }

        #region поля и свойства

        public UserName UserName { get; set; } = null!;
        public Email Email { get; set; } = null!;

        /// <summary>
        /// Хэшированный пароль пользователя. Пароль хранится в хэшированном виде для безопасности.
        /// </summary>
        public PasswordHash PasswordHash { get; set; } = null!;
        public PhoneNumber? PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; }
        public Guid SystemRoleId { get; set; }
        /// <summary>
        /// Ссылка на системную роль (по умолчанию значение null, что означает, 
        /// что у пользователя нет специфической роли, дающей или блокирущей 
        /// возможности пользоваться системой)
        /// </summary>
        public SystemRole? SystemRole { get; set; }

        #region PersonalChat
        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User1
        /// </summary>
        readonly List<Chat.PersonalChat> _personalChatsAsUser1;
        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User2
        /// </summary>
        readonly List<Chat.PersonalChat> _personalChatsAsUser2;
        private IEnumerable<Chat.PersonalChat> PrivatePersonalChats
        {
            get
            {
                if (_personalChatsAsUser1 != null)
                    foreach (Chat.PersonalChat pc in _personalChatsAsUser1)
                        yield return pc;
                if (_personalChatsAsUser2 != null)
                    foreach (Chat.PersonalChat pc in _personalChatsAsUser2)
                        yield return pc;
            }
        }
        /// <summary>
        /// Чаты, где пользователь является User1
        /// </summary>
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChatsAsUser1 => _personalChatsAsUser1;
        /// <summary>
        /// Чаты, где пользователь является User2
        /// </summary>
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChatsAsUser2 => _personalChatsAsUser2;
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChats => [.. PrivatePersonalChats]; // приводит тип к списку
        #endregion

        #endregion

        /// <summary>
        /// Метод создания нового пользователя
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="phoneNumber">Номер телефона, можно оставить null</param>
        /// <param name="systemRole">Роль, можно оставить null</param>
        /// <returns>Новый экземпляр класс <see cref="User"/></returns>
        public static User Create(
            Guid id,
            UserName userName,
            Email email,
            Password password,
            PhoneNumber? phoneNumber,
            SystemRole? systemRole
            )
        {
            var user = new User(id, userName, email, password, phoneNumber, systemRole);

            // Генерация события регистрации пользователя
            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

            return user;
        }

        public User UpdateRole(SystemRole role)
        {
            this.SystemRole = role;

            this.RaiseDomainEvent(new UserRoleChangedDomainEvent(this.Id));

            return this;
        }

        public User ChangeName(UserName UserName)
        {
            this.UserName = UserName;

            this.RaiseDomainEvent(new UserNameChangedDomainEvent(this.Id));

            return this;
        }

        public User ChangePassword(Password Password)
        {
            this.PasswordHash = PasswordHash.Create(Password).Value;

            this.RaiseDomainEvent(new UserPasswordChangedDomainEvent(this.Id));

            return this;
        }

        public User ChangePhoneNumber(PhoneNumber PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;

            this.RaiseDomainEvent(new UserPhoneNumberChangedDomainEvent(this.Id));

            return this;
        }
    }
}