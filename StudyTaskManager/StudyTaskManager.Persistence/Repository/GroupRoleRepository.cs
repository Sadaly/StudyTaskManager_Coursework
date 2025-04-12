using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class GroupRoleRepository : Generic.TWithIdRepository<GroupRole>, IGroupRoleRepository
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
            bool notUniqueName = await _dbSet.AnyAsync(gr => gr.RoleName.Value == entity.RoleName.Value, cancellationToken);
            if (notUniqueName) { return Result.Failure(PersistenceErrors.GroupRole.NotUniqueName); }

            if (entity.GroupId != null)
            {
                var group = await GetFromDBAsync<Group>(entity.GroupId.Value, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
                if (group.IsFailure) { return group; }
            }

            var groupRole = await GetFromDBAsync(entity.Id, cancellationToken);
            if (groupRole.IsSuccess) { return Result.Failure(PersistenceErrors.GroupRole.AlreadyExist); }
            return Result.Success();
        }
    }
}
