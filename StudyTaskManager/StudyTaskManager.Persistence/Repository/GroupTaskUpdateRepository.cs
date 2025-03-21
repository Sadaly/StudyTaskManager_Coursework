using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskUpdateRepository : Generic.TWithIdRepository<GroupTaskUpdate>, IGroupTaskUpdateRepository
    {
        public GroupTaskUpdateRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<GroupTaskUpdate>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
