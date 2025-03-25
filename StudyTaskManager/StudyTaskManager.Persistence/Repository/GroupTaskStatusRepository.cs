using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskStatusRepository : Generic.TWithIdRepository<GroupTaskStatus>, IGroupTaskStatusRepository
    {
        public GroupTaskStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(Group group, bool togetherWithTheGeneral, CancellationToken cancellationToken = default)
        {
            if (togetherWithTheGeneral)
            {
                return await _dbContext.Set<GroupTaskStatus>()
                    .Where(gts => gts.GroupId == group.Id || gts.GroupId == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupTaskStatus>>> GetByWithoutGroupAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
