using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskStatusRepository : Generic.IRepositoryWithID<GroupTaskStatus>
    {
        /// <summary>
        /// Выдать все статусы внутри группы.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);
        /// <summary>
        /// Выдать часть статусов внутри группы.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default);


        /// <summary>
        /// Выдать все статусы внутри группы вместе с базовыми.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default);
        /// <summary>
        /// Выдать часть статусов внутри группы и базовых.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать все общие статусы.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetBaseAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Выдать часть общих статусов.
        /// </summary>
        Task<Result<List<GroupTaskStatus>>> GetBaseAsync(int startIndex, int count, CancellationToken cancellationToken = default);
    }
}
