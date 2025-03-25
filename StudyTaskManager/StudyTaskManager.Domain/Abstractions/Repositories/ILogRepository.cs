using StudyTaskManager.Domain.Entity.Log;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface ILogRepository : Generic.IRepositoryWithID<Log>
    {
        /// <summary>
        /// Получить log вместе с logAction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>экземпляр класса log вместе с не null свойством LogAction</returns>
        Task<Result<Log>> GetByIdWithLogActionAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
