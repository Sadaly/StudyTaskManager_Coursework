using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskUpdateRepository : Generic.TWithIdRepository<GroupTaskUpdate>, IGroupTaskUpdateRepository
    {
        public GroupTaskUpdateRepository(AppDbContext dbContext) : base(dbContext) { }

        public override async Task<Result> AddAsync(GroupTaskUpdate groupTaskUpdate, CancellationToken cancellationToken = default)
        {
            // Проверка при добавлении на возможность апдейта у статуса задачи
            GroupTask? gt = await _dbContext.Set<GroupTask>().FirstOrDefaultAsync(gt => gt.Id == groupTaskUpdate.TaskId, cancellationToken);
            if (gt == null)
                return Result.Failure(new Error(
                    $"{typeof(GroupTaskUpdate)}.NullValue",
                    $"Задачи для которой создается апдейт не существует."));

            GroupTaskStatus? gts = await _dbContext.Set<GroupTaskStatus>().FirstOrDefaultAsync(gts => gts.Id == gt.StatusId, cancellationToken);

            if (gts != null && !gts.CanBeUpdated)
                return Result.Failure(new Error(
                    $"{typeof(GroupTaskUpdate)}.{typeof(GroupTaskStatus)}.CantBeUpdated",
                    "Статус задачи не позволяет создавать новые апдейты."));

            await _dbContext.Set<GroupTaskUpdate>().AddAsync(groupTaskUpdate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskUpdate>()
                .Where(gtu => gtu.TaskId == groupTask.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
