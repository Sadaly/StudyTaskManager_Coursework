using StudyTaskManager.Domain.Entity.Group.GroupTask;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskUpdateRepository : Generic.IRepositoryWithID<GroupTaskUpdate>
    {
        /// <summary>
        /// Возвращает список апдейтов для задачи.
        /// </summary>
        Task<List<GroupTaskUpdate>> GetByTaskAsync(GroupTask groupTask);
    }
}
