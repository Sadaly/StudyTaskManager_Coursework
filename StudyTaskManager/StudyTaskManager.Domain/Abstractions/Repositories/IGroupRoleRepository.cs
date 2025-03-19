using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupRoleRepository : Generic.IRepositoryWithID<GroupRole>
    {
        /// <summary>
        /// Выдать роли группы.
        /// </summary>
        Task<List<GroupRole>> GetByGroupAsync(Group group);

        /// <summary>
        /// Выдать роли общие для всех групп.
        /// </summary>
        Task<List<GroupRole>> GetByWithoutGroupAsync();
    }
}
