using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskStatusRepository : Generic.IRepositoryWithID<GroupTaskStatus>
    {
        /// <summary>
        /// Выдать статусы внутри группы.
        /// </summary>
        Task<List<GroupTaskStatus>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать общие статусы.
        /// </summary>
        Task<List<GroupTaskStatus>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default);
    }
}
