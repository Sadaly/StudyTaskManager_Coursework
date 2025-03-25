using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupRoleRepository : Generic.IRepositoryWithID<GroupRole>
    {
        /// <summary>
        /// Выдать роли группы.
        /// </summary>
        Task<Result<List<GroupRole>>> GetByGroupAsync(Group group, bool togetherWithTheGeneral, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать роли общие для всех групп.
        /// </summary>
        Task<Result<List<GroupRole>>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default);
    }
}
