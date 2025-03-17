namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="User">Ссылка на группу</param>
        void Add(Entity.User.UserBase User);
    }
}
