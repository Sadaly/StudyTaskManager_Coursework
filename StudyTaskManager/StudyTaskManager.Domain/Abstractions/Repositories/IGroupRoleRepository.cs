using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupRoleRepository : Generic.IRepositoryWithID<GroupRole>
    {
        /// <summary>
        /// Выдать роли группы.
        /// </summary>
        Task<Result<List<GroupRole>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать роли группы вместе с базовыми.
        /// </summary>
        Task<Result<List<GroupRole>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать роли общие для всех групп.
        /// </summary>
        Task<Result<List<GroupRole>>> GetBaseAsync(CancellationToken cancellationToken = default);
    }
}
