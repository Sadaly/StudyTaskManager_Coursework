using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskStatusRepository : Generic.IRepositoryWithID<GroupTaskStatus>
    {
        /// <summary>
        /// Выдать статусы внутри группы.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать статусы внутри группы вместе с базовыми.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать общие статусы.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetBaseAsync(CancellationToken cancellationToken = default);
    }
}
