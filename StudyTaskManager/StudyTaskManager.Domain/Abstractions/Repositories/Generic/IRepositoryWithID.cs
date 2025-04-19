using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories.Generic
{
    /// <summary>
    /// Базовый интерфейс репозиториев для сущностей с Id.
    /// </summary>
    /// <typeparam name="T">Класс для работы.</typeparam>
    public interface IRepositoryWithID<T> : IRepository<T> where T : Common.BaseEntityWithID
    {
        /// <summary>
        /// Возвращает объект по Id.
        /// </summary>
        /// <returns>Если объект не найден, то будет возвращена ошибка.</returns>
        Task<Result<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаление экземпляра по Id.
        /// </summary>
        /// <param name="entityId">Id сущности.</param>
        Task<Result> RemoveAsync(Guid entityId, CancellationToken cancellationToken = default);
    }
}