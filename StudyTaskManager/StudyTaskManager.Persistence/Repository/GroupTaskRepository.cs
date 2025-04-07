using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Errors;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskRepository : Generic.TWithIdRepository<GroupTask>, IGroupTaskRepository
    {
        public GroupTaskRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            if (groupTask.ParentId == groupTask.Id) return Result.Failure(PersistenceErrors.GroupTask.СannotParentForItself);

            Group? group = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupTask.GroupId, cancellationToken);
            if (group == null) return Result.Failure(PersistenceErrors.Group.NotFound);

            GroupTaskStatus? groupTaskStatus = await _dbContext.Set<GroupTaskStatus>().FirstOrDefaultAsync(gts => gts.Id == groupTask.StatusId, cancellationToken);
            if (groupTaskStatus == null) return Result.Failure(PersistenceErrors.GroupTaskStatus.NotFound);

            // TODO добавить проверку, чтобы ответственный за задачу был обязан присутствовать в группе

            await _dbContext.Set<GroupTask>().AddAsync(groupTask, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<List<GroupTask>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTask>()
                .Where(gt => gt.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
