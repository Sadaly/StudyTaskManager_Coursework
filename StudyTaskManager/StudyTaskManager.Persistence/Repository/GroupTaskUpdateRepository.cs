using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupTaskUpdateRepository : Generic.TWithIdRepository<GroupTaskUpdate>, IGroupTaskUpdateRepository
    {
        public GroupTaskUpdateRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskUpdate>()
                .Where(gtu => gtu.TaskId == groupTask.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupTaskUpdate>>> GetByTaskAsync(int startIndex, int count, GroupTask groupTask, CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(
                startIndex,
                count,
                gtu => gtu.TaskId == groupTask.Id,
                cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupTaskUpdate.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupTaskUpdate.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupTaskUpdate entity, CancellationToken cancellationToken)
        {
            Result<User> creator = await GetFromDBAsync<User>(entity.CreatorId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (creator.IsFailure) { return creator; }

            Result<GroupTask> groupTask = await GetFromDBAsync<GroupTask>(entity.TaskId, PersistenceErrors.GroupTask.IdEmpty, PersistenceErrors.GroupTask.NotFound, cancellationToken);
            if (groupTask.IsFailure) { return groupTask; }

            Result<GroupTaskStatus> groupTaskStatus = await GetFromDBAsync<GroupTaskStatus>(
                groupTask.Value.StatusId,
                PersistenceErrors.GroupTaskStatus.IdEmpty,
                PersistenceErrors.GroupTaskStatus.NotFound,
                cancellationToken);
            if (groupTaskStatus.IsFailure) { return groupTaskStatus; }
            if (!groupTaskStatus.Value.CanBeUpdated) { return Result.Failure(PersistenceErrors.GroupTaskStatus.CantBeUpdated); }

            Result<GroupTaskUpdate> groupTaskUpdate = await GetFromDBAsync(entity.Id, cancellationToken);
            if (groupTaskUpdate.IsSuccess) { return Result.Failure(PersistenceErrors.GroupTaskUpdate.AlreadyExists); }
            return Result.Success();
        }
    }
}
