using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskUpdateRepository : Generic.TWithIdRepository<GroupTaskUpdate>, IGroupTaskUpdateRepository
    {
        public GroupTaskUpdateRepository(AppDbContext dbContext) : base(dbContext) { }
        // TODO проверка при добавлении на возможность апдейта у статуса задачи
        public async Task<List<GroupTaskUpdate>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskUpdate>()
                .AsNoTracking()
                .Where(gtu => gtu.TaskId == groupTask.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
