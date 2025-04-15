using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface ISystemRoleRepository : Generic.IRepositoryWithID<SystemRole>
    {
        /// <summary>
        /// Возвращает системную роль по названию.
        /// </summary>
        /// <returns>Если объект не найден, то ошибка.</returns>
        Task<Result<SystemRole>> GetByTitleAsync(Title title, CancellationToken cancellationToken = default);
    }
}
