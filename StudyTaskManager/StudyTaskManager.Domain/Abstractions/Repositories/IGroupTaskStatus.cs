using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskStatus : Generic.IRepositoryWithID<GroupTaskStatus>
    {
        /// <summary>
        /// Выдать статусы внутри группы.
        /// </summary>
        public Task<List<GroupTaskStatus>> GetByGroupAsync(Group group);

        /// <summary>
        /// Выдать общие статусы.
        /// </summary>
        public Task<List<GroupTaskStatus>> GetByWithoutGroupAsync();
    }
}
