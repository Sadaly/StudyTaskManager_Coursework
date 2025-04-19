using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskUpdateRepository : Generic.IRepositoryWithID<GroupTaskUpdate>
    {
        /// <summary>
        /// Возвращает список всех апдейтов для задачи.
        /// </summary>
        Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает список части апдейтов для задачи.
        /// </summary>
        Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(int startIndex, int count, GroupTask groupTask, CancellationToken cancellationToken = default);
    }
}
