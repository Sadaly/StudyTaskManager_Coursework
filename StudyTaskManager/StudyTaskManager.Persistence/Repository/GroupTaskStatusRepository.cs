using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupTaskStatusRepository : Generic.TWithIdRepository<GroupTaskStatus>, IGroupTaskStatusRepository
    {
        public GroupTaskStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupTaskStatus>>> GetBaseAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                    .Where(gts => gts.GroupId == group.Id || gts.GroupId == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupTaskStatus.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupTaskStatus.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupTaskStatus entity, CancellationToken cancellationToken)
        {
            bool notUniqueName = await _dbContext.Set<GroupTaskStatus>().AnyAsync(gts => gts.Name.Value == entity.Name.Value, cancellationToken);
            if (notUniqueName) { return Result.Failure(PersistenceErrors.GroupTaskStatus.NotUniqueName); }

            Result<object> obj;

            if (entity.GroupId != null)
            {
                obj = await GetFromDBAsync<Group>((Guid)entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
                if (obj.IsFailure) { return obj; }
            }

            obj = await GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.GroupTaskStatus.AlreadyExists);
        }
    }
}
