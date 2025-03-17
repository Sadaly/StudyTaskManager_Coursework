using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Абстрактный класс пользователя
    /// </summary>
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой-нибудь паттерн, строитель или фабрику.
    public class User : BaseEntityWithID
    {
        /// <summary>
        /// Конструктор класс User
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

            //Todo оставшаяся реализация, связанная с чатами
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public UserName UserName { get; set; } = null!;

        /// <summary>
        /// Id системной роли
        /// </summary>
        public Guid SystemRoleId { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public Email Email { get; set; } = null!;

        /// <summary>
        /// Хэшированный пароль пользователя
        /// </summary>
        public PasswordHash PasswordHash { get; set; } = null!;

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public PhoneNumber? PhoneNumber { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; }


        /// <summary>
        /// Ссылка на системную роль (по умолчанию значение null, что означает, что у пользователя нет специфической роли, дающей или блокирущей возможности пользоваться системой)
        /// </summary>
        public SystemRole? SystemRole { get; set; }

        /// <summary>
        /// Ссылка на личные чаты
        /// </summary>
        public IReadOnlyCollection<Chat.PersonalChat>? PersonalChat => _personalChat;
        private List<Chat.PersonalChat>? _personalChat;

        /// <summary>
        /// Метод создания нового пользователя
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="phoneNumber">Номер телефона, можно оставить null</param>
        /// <param name="systemRole">Роль, можно оставить null</param>
        /// <returns></returns>
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

            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

            return user;
        }
    }
}
