using StudyTaskManager.Domain.Entity.Group.GroupTask;
using System.Text.RegularExpressions;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskRepository : Generic.IRepositoryWithID<GroupTask>
    {
        /// <summary>
        /// Получить все задачи группы.
        /// </summary>
        Task<List<GroupTask>> GetByGroupAsync(Group group);
    }
}
