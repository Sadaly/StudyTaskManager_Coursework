using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupInviteRepository : Generic.IRepository<GroupInvite>
    {
        /// <summary>
        /// Выдать список приглашений для пользователя.
        /// </summary>
        /// <param name="user">Пользователь, которого приглашают в группу.</param>
        Task<List<GroupInvite>> GetForUserAsync(User user);
        
        /// <summary>
        /// Выдать список приглашений от пользователя.
        /// </summary>
        /// <param name="user">Пользователь, который приглашает в группу.</param>
        Task<List<GroupInvite>> GetFromUserAsync(User user);

        /// <summary>
        /// Выдать список приглашений в группе.
        /// </summary>
        Task<List<GroupInvite>> GetByUserAsync(Group group);
    }
}
