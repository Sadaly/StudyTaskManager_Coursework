using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupRepository : Generic.TWithIdRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext) { }

        protected override Error GetErrorIdEmpty()
        {
            return PersistenceErrors.Group.IdEmpty;
        }

        protected override Error GetErrorNotFound()
        {
            return PersistenceErrors.Group.NotFound;
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(Group entity, CancellationToken cancellationToken)
        {
            var groupRole = await GetFromDBAsync<GroupRole>(entity.DefaultRoleId, PersistenceErrors.GroupRole.IdEmpty, PersistenceErrors.GroupRole.NotFound, cancellationToken);
            if (groupRole.IsFailure) { return groupRole; }

            var group = await GetFromDBAsync(entity.Id, cancellationToken);
            if (group.IsSuccess) { return Result.Failure(PersistenceErrors.Group.AlreadyExists); }
            return Result.Success();
        }
    }
}
