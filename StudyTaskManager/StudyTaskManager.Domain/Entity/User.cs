namespace StudyTaskManager.Domain.Entity
{
    // Абстрактный класс пользователя.
    // Нужно будет добавить две его реализации: обычный пользователь и заблокированный.
    // Для создания экземпляров нужно будет использовать какой нибудь паттерн, строитель или фабрику.
    abstract class User
    {
        public string UserName { get; }
        public SystemRole SystemRole { get; }
        public string Email { get; }
        public string NumberPhone { get; }
        public DateTime RegistrationDate { get; }
        public IReadOnlyCollection<PersonalChat> PersonalChat => _personalChat;
        private List<PersonalChat> _personalChat;
     }
}
