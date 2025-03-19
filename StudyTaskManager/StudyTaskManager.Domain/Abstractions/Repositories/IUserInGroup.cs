using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IUserInGroup : Generic.IRepository<UserInGroup>
    {
        /// <summary>
        /// Выдать всех пользователей в группе.
        /// </summary>
        /// <param name="group">Группа в которую должны входить пользователи.</param>
        public Task<List<UserInGroup>> GetByGroupAsync(Group group);

        /// <summary>
        /// Выдать все группы пользователя.
        /// </summary>
        /// <param name="user">Пользователь который должен находиться в группе.</param>
        public Task<List<UserInGroup>> GetByUserAsync(User user);
    }
}
