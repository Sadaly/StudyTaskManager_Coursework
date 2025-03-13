using System.Text.RegularExpressions;

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
        void Add(Group Group);

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <param name="Group">Ссылка на группу</param>
        void Remove(Group Group);
    }
}
