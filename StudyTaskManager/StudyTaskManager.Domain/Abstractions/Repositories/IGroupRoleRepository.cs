using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupRoleRepository : Generic.IRepositoryWithID<GroupRole>
    {
        /// <summary>
        /// Выдать роли группы.
        /// </summary>
        public Task<List<GroupRole>> GetByGroupAsync(Group group);

        /// <summary>
        /// Выдать роли общие для всех групп.
        /// </summary>
        public Task<List<GroupRole>> GetByWithoutGroupAsync();
    }
}
