using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupTaskStatusRepository : Generic.TWithIdRepository<GroupTaskStatus>, IGroupTaskStatusRepository
    {
        public GroupTaskStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<Result<List<GroupTaskStatus>>> GetByGroupAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(
                startIndex,
                count,
                gts => gts.GroupId == group.Id,
                cancellationToken);
        }

        public async Task<Result<List<GroupTaskStatus>>> GetBaseAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                .Where(gts => gts.GroupId == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<Result<List<GroupTaskStatus>>> GetBaseAsync(int startIndex, int count, CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(
                startIndex,
                count,
                gts => gts.GroupId == null,
                cancellationToken);
        }

        public async Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupTaskStatus>()
                    .Where(gts => gts.GroupId == group.Id || gts.GroupId == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }
        public async Task<Result<List<GroupTaskStatus>>> GetByGroupWithBaseAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default)
        {

            return await GetFromDBWhereAsync(
                startIndex,
                count,
                gts => gts.GroupId == group.Id || gts.GroupId == null,
                cancellationToken);
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

            if (entity.GroupId != null)
            {
                var group = await GetFromDBAsync<Group>(entity.GroupId.Value, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
                if (group.IsFailure) { return group; }
            }

            var groupTaskStatus = await GetFromDBAsync(entity.Id, cancellationToken);
            if (groupTaskStatus.IsSuccess) { return Result.Failure(PersistenceErrors.GroupTaskStatus.AlreadyExists); }
            return Result.Success();
        }
    }
}
