namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Хранилище групп
    /// </summary>
    public interface IGroupRepository
    {
        /// <summary>
        /// Добавление новой группы
        /// </summary>
        /// <param name="Group">Ссылка на группу</param>
        void Add(Entity.Group.Group Group);

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <param name="Group">Ссылка на группу</param>
        void Remove(Entity.Group.Group Group);
    }
}
