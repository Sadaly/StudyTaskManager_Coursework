using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Persistence.DB;
using System.Text.RegularExpressions;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskRepository : Generic.TWithIdRepository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public Task<List<GroupTask>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
