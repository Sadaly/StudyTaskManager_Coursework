using StudyTaskManager.Domain.Common.Interfaces;

namespace StudyTaskManager.Domain.Entity
{
    // Абстрактный класс пользователя.
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой нибудь паттерн, строитель или фабрику.
    public abstract class User : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Id системной роли
        /// </summary>
        public int SystemRoleId { get; set; }

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
        public IReadOnlyCollection<PersonalChat>? PersonalChat => _personalChat;
        private List<PersonalChat>? _personalChat;
     }
}
