using StudyTaskManager.Domain.Common;

namespace StudyTaskManager.Domain.Entity.User
{
    /// <summary>
    /// Абстрактный класс пользователя
    /// </summary>
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой-нибудь паттерн, строитель или фабрику.
    public abstract class AbsUser : BaseEntityWithID
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Id системной роли
        /// </summary>
        public Guid SystemRoleId { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string? NumberPhone { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; }


        /// <summary>
        /// Ссылка на системную роль
        /// </summary>
        public SystemRole SystemRole { get; set; } = null!;

        /// <summary>
        /// Ссылка на личные чаты
        /// </summary>
        public IReadOnlyCollection<Chat.PersonalChat>? PersonalChat => _personalChat;
        private List<Chat.PersonalChat>? _personalChat;
    }
}
