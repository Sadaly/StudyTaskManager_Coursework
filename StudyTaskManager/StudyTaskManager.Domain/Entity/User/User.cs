using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Common;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Errors;
using System.Text.Json.Serialization;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User : BaseEntityWithID
    {
        private User(Guid id) : base(id)
        {
            _personalChatsAsUser1 = [];
            _personalChatsAsUser2 = [];
        }
        /// <summary>
        /// Конструктор класс <see cref="User"/>
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="phoneNumber">Номер телефона, можно оставить null</param>
        /// <param name="systemRole">Роль, можно оставить null</param>
        private User(Guid id, Username username, Email email, Password password, DateTime registrationDate, PhoneNumber? phoneNumber, SystemRole? systemRole) : this(id)
        {
            Username = username;
            Email = email;
            PasswordHash = PasswordHash.Create(password).Value;
            RegistrationDate = registrationDate;

            if (phoneNumber == null) { PhoneNumber = PhoneNumber.CreateDefault(); }
            else { PhoneNumber = phoneNumber; }

            if (systemRole != null)
            {
                SystemRoleId = systemRole.Id;
                SystemRole = systemRole;
            }
        }

        #region поля и свойства

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public Username Username { get; set; } = null!;

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public Email Email { get; set; } = null!;

        /// <summary>
        /// Переменная показывает подтвержден ли пользовательский email
        /// </summary>
        public bool IsEmailVerifed { get; private set; }

        /// <summary>
        /// Переменная показывает подтвержден ли пользовательский номер телефона
        /// </summary>
        public bool IsPhoneNumberVerifed { get; private set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public PhoneNumber PhoneNumber { get; set; }

        /// <summary>
        /// Дата регистрации пользователя. Устанавливается автоматически при создании пользователя.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Хэшированный пароль пользователя. Пароль хранится в хэшированном виде для безопасности.
        /// </summary>
        public PasswordHash PasswordHash { get; set; } = null!;

        /// <summary>
        /// Id системной роли (может не быть)
        /// </summary>
        public Guid? SystemRoleId { get; set; }
        /// <summary>
        /// Ссылка на системную роль (по умолчанию значение null, что означает, 
        /// что у пользователя нет специфической роли, дающей или блокирущей 
        /// возможности пользоваться системой)
        /// </summary>
        [JsonIgnore]
        public SystemRole? SystemRole { get; set; }

        #region PersonalChat
        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User1
        /// </summary>
        private readonly List<Chat.PersonalChat> _personalChatsAsUser1;
        /// <summary>
        /// Приватное поле для хранения списка личных чатов, где пользователь является User2
        /// </summary>
        private readonly List<Chat.PersonalChat> _personalChatsAsUser2;
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
        [JsonIgnore]
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChatsAsUser1 => _personalChatsAsUser1;
        /// <summary>
        /// Чаты, где пользователь является User2
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChatsAsUser2 => _personalChatsAsUser2;
        public IReadOnlyCollection<Chat.PersonalChat> PersonalChats => [.. PrivatePersonalChats]; // [.. PrivatePersonalChats] приводит тип к списку

        #endregion
        #endregion

        /// <summary>
        /// Метод создания нового пользователя
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="email">Почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="phoneNumber">Номер телефона, можно оставить null</param>
        /// <param name="systemRole">Роль, можно оставить null</param>
        /// <returns>Новый экземпляр класс <see cref="User"/></returns>
        public static Result<User> Create(Username username, Email email, Password password, PhoneNumber? phoneNumber, SystemRole? systemRole)
        {
            var user = new User(Guid.Empty, username, email, password, DateTime.UtcNow, phoneNumber, systemRole);

            // Генерация события регистрации пользователя
            user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

            return Result.Success(user);
        }

        public Result ChangeSystemRole(SystemRole role)
        {
            this.SystemRole = role;

            this.RaiseDomainEvent(new UserSystemRoleChangedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result ChangeUsername(Username Username)
        {
            this.Username = Username;

            this.RaiseDomainEvent(new UsernameChangedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result ChangePassword(Password Password)
        {
            var ph = PasswordHash.Create(Password).Value;

            if (ph.Value == PasswordHash.Value)
                return Result.Failure(DomainErrors.Password.Match);

            this.PasswordHash = ph;

            this.RaiseDomainEvent(new UserPasswordChangedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result ChangePhoneNumber(PhoneNumber PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;

            this.RaiseDomainEvent(new UserPhoneNumberChangedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result VerifyEmail()
        {
            if (IsEmailVerifed)
                return Result.Failure(DomainErrors.Email.AlreadyVerified);

            IsEmailVerifed = true;

            this.RaiseDomainEvent(new UserEmailVerifiedDomainEvent(this.Id));

            return Result.Success();
        }

        public Result VerifyPhoneNumber()
        {
            if (IsPhoneNumberVerifed)
                return Result.Failure(DomainErrors.PhoneNumber.AlreadyVerified);

            IsPhoneNumberVerifed = true;

            this.RaiseDomainEvent(new UserPhoneNumberlVerifiedDomainEvent(this.Id));

            return Result.Success();
        }
    }
}