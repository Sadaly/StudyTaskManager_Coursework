using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskStatusRepository : Generic.TWithIdRepository<GroupTaskStatus>, IGroupTaskStatusRepository
    {
        public GroupTaskStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<GroupTaskStatus>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .AsNoTracking()
                .Where(gts => gts.GroupId == group.Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<GroupTaskStatus>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .AsNoTracking()
                .Where(gts => gts.GroupId == null)
                .ToListAsync(cancellationToken);
        }
    }
}
