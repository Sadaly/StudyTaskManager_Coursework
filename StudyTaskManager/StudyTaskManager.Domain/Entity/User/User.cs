using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой-нибудь паттерн, строитель или фабрику.
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
        private User(Guid id, UserName userName, Email email, Password password, PhoneNumber? phoneNumber, SystemRole? systemRole)
            : base(id)
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
            _personalChatsAsUser1 = new List<Chat.PersonalChat>();
            _personalChatsAsUser2 = new List<Chat.PersonalChat>();
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public UserName UserName { get; set; } = null!;

        /// <summary>
        /// Id системной роли
        /// </summary>
        public Guid? SystemRoleId { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public Email Email { get; set; } = null!;

        /// <summary>
        /// Хэшированный пароль пользователя. Пароль хранится в хэшированном виде для безопасности.
        /// </summary>
        public PasswordHash PasswordHash { get; set; } = null!;

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// Дата регистрации пользователя. Устанавливается автоматически при создании пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; }

        /// <summary>
        /// Ссылка на системную роль (по умолчанию значение null, что означает, что у пользователя нет специфической роли, дающей или блокирущей возможности пользоваться системой)
        /// </summary>
        public SystemRole? SystemRole { get; set; }

        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User1
        /// </summary>
        private List<Chat.PersonalChat>? _personalChatsAsUser1;

        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User2
        /// </summary>
        private List<Chat.PersonalChat>? _personalChatsAsUser2;

        /// <summary>
        /// Чаты, где пользователь является User1
        /// </summary>
        public ICollection<PersonalChat>? PersonalChatsAsUser1 => _personalChatsAsUser1;

        /// <summary>
        /// Чаты, где пользователь является User2
        /// </summary>
        public ICollection<PersonalChat>? PersonalChatsAsUser2 => _personalChatsAsUser2;

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
		public BlockedUserInfo BlockUser(string reason)
		{
			return BlockedUserInfo.Create(reason, this);
		}
	}
}