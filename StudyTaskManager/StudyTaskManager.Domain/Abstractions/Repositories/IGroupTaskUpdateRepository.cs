using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupTaskUpdateRepository : Generic.IRepositoryWithID<GroupTaskUpdate>
    {
        /// <summary>
        /// Возвращает список апдейтов для задачи.
        /// </summary>
        public Task<List<GroupTaskUpdate>> GetByTaskAsync(GroupTask groupTask);
    }
}
