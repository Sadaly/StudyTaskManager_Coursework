using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.GroupTask;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskStatusRepository : Generic.IRepositoryWithID<GroupTaskStatus>
    {
        /// <summary>
        /// Выдать статусы внутри группы.
        /// </summary>
        Task<List<GroupTaskStatus>> GetByGroupAsync(Group group);

        /// <summary>
        /// Выдать общие статусы.
        /// </summary>
        Task<List<GroupTaskStatus>> GetByWithoutGroupAsync();
    }
}
