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

        public async Task<Result<List<GroupTask>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTask>()
                .Where(gt => gt.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupTask.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupTask.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupTask entity, CancellationToken cancellationToken)
        {
            if (entity.ParentId == entity.Id) return Result.Failure(PersistenceErrors.GroupTask.СannotParentForItself);

            Result<object> obj;

            obj = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync<GroupTaskStatus>(entity.StatusId, PersistenceErrors.GroupTaskStatus.IdEmpty, PersistenceErrors.GroupTaskStatus.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            // TODO добавить проверку, чтобы ответственный за задачу был обязан присутствовать в группе

            obj = await GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.GroupTaskStatus.AlreadyExists);
        }
    }
}
