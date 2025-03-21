using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskRepository : Generic.TWithIdRepository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<GroupTask>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTask>()
                .AsNoTracking()
                .Where(gt => gt.GroupId == group.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
