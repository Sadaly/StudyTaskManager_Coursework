namespace StudyTaskManager.Domain.Entity
{
    // Абстрактный класс пользователя.
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой нибудь паттерн, строитель или фабрику.
    public abstract class User
    {
        public string UserName { get; } = null!;
        public SystemRole SystemRole { get; } = null!;
        public string Email { get; } = null!;
        public string NumberPhone { get; } = null!;
        public DateTime RegistrationDate { get; }
        public IReadOnlyCollection<PersonalChat>? PersonalChat => _personalChat;
        private List<PersonalChat>? _personalChat;
     }
}
