using StudyTaskManager.Domain.Entity.Log;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface ILogRepository : Generic.IRepositoryWithID<Log>
    {
        /// <summary>
        /// Получить log вместе с logAction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>экземпляр класса log вместе с не null свойством LogAction</returns>
        Task<Log> GetByIdWithLogActionAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
