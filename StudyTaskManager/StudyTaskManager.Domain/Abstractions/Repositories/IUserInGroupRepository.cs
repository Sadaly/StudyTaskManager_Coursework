using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IUserInGroupRepository : Generic.IRepository<UserInGroup>
    {
        /// <summary>
        /// Выдать все группы пользователя.
        /// </summary>
        /// <param name="user">Пользователь который должен находиться в группе.</param>
        Task<Result<List<UserInGroup>>> GetByUserAsync(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать всех пользователей в группе.
        /// </summary>
        /// <param name="group">Группа в которую должны входить пользователи.</param>
        Task<Result<List<UserInGroup>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать пользователя в группе.
        /// </summary>
        Task<Result<UserInGroup>> GetByUserAndGroupAsync(User user, Group group, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать пользователя в группе.
        /// </summary>
        Task<Result<UserInGroup>> GetByUserAndGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    }
}
