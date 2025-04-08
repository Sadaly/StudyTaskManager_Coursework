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

        public override async Task<Result> AddAsync(GroupTaskStatus groupTaskStatus, CancellationToken cancellationToken = default)
        {
            if (groupTaskStatus.GroupId != null)
            {
                Group? group = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupTaskStatus.GroupId, cancellationToken);
                if (group == null) return Result.Failure(PersistenceErrors.Group.NotFound);
            }

            await _dbContext.Set<GroupTaskStatus>().AddAsync(groupTaskStatus, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
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
            // TODO

            //bool notUniqueName = await _dbContext.Set<GroupRole>().AnyAsync(gr => gr.RoleName.Value == groupRole.RoleName.Value, cancellationToken);
            //if (notUniqueName) return Result.Failure(PersistenceErrors.GroupRole.NotUniqueName);

            throw new NotImplementedException();
        }
    }
}
