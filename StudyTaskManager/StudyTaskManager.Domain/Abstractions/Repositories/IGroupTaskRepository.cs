using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskRepository : Generic.IRepositoryWithID<GroupTask>
    {
        /// <summary>
        /// Получить все задачи группы.
        /// </summary>
        Task<Result<List<GroupTask>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);
    }
}
