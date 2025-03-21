using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskStatusRepository : Generic.TWithIdRepository<GroupTaskStatus>, IGroupTaskStatusRepository
    {
        public GroupTaskStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<GroupTaskStatus>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupTaskStatus>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
