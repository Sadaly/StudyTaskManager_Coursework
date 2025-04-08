using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.Group.Task;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRoleRepository : Generic.TWithIdRepository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupRole>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                .Where(gr => gr.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupRole>>> GetBaseAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                .Where(gr => gr.GroupId == null)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupRole>>> GetByGroupWithBaseAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupRole>()
                    .Where(gr => gr.GroupId == group.Id || gr.GroupId == null)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
        }

        public override async Task<Result> AddAsync(GroupRole groupRole, CancellationToken cancellationToken = default)
        {
            if (groupRole.GroupId != null)
            {
                Group? group = await _dbContext.Set<Group>().FirstOrDefaultAsync(g => g.Id == groupRole.GroupId, cancellationToken);
                if (group == null) return Result.Failure(PersistenceErrors.Group.NotFound);
            }

            bool notUniqueName = await _dbContext.Set<GroupRole>().AnyAsync(gr => gr.RoleName.Value == groupRole.RoleName.Value, cancellationToken);
            if (notUniqueName) return Result.Failure(PersistenceErrors.GroupRole.NotUniqueName);

            await _dbContext.Set<GroupRole>().AddAsync(groupRole, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.GroupRole.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.GroupRole.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupRole entity, CancellationToken cancellationToken)
        {
            bool notUniqueName = await _dbContext.Set<GroupRole>().AnyAsync(gr => gr.RoleName.Value == entity.RoleName.Value, cancellationToken);
            if (notUniqueName) { return Result.Failure(PersistenceErrors.GroupRole.NotUniqueName); }

            Result<object> obj;

            if (entity.GroupId != null)
            {
                obj = await GetFromDBAsync<Group>((Guid)entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
                if (obj.IsFailure) { return obj; }
            }

            obj = await GetFromDBAsync(entity.Id, cancellationToken);
            if (obj.IsFailure)
            {
                if (obj.Error == GetErrorNotFound()) return Result.Success();
                return obj;
            }
            return Result.Failure(PersistenceErrors.GroupRole.AlreadyExist);
        }
    }
}
