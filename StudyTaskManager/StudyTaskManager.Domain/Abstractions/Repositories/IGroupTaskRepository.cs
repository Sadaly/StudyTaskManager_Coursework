using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskRepository : Generic.IRepositoryWithID<GroupTask>
    {
        /// <summary>
        /// Получить все задачи группы.
        /// </summary>
        Task<List<GroupTask>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);
    }
}
