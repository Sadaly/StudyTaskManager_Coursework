using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskUpdateRepository : Generic.IRepositoryWithID<GroupTaskUpdate>
    {
        /// <summary>
        /// Возвращает список апдейтов для задачи.
        /// </summary>
        Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default);
    }
}
