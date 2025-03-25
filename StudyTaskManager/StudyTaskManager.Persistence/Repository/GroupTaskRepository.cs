using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskRepository : Generic.TWithIdRepository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTask>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTask>()
                .Where(gt => gt.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
