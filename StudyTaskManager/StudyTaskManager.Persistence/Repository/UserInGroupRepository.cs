using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<UserInGroup>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<UserInGroup>> GetByUserAndGroupAsync(User user, Group group, CancellationToken cancellationToken = default)
        {
            return await GetByUserAndGroupAsync(user.Id, group.Id, cancellationToken);
        }

        public async Task<Result<UserInGroup>> GetByUserAndGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            Result<object> obj;

            obj = await GetFromDBAsync<User>(userId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (obj.IsFailure) return Result.Failure<UserInGroup>(obj.Error);

            obj = await GetFromDBAsync<Group>(groupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (obj.IsFailure) return Result.Failure<UserInGroup>(obj.Error);
            return await GetFromDBAsync(
                uig =>
                    uig.UserId == userId &&
                    uig.GroupId == groupId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
        }

        public async Task<Result<List<UserInGroup>>> GetByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.UserId == user.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            Result obj;

            obj = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync<GroupRole>(entity.RoleId, PersistenceErrors.GroupRole.IdEmpty, PersistenceErrors.GroupRole.NotFound, cancellationToken);
            if (obj.IsFailure) { return obj; }

            obj = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId &&
                    uig.RoleId == entity.RoleId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            if (obj.IsFailure) { return obj; }
            return Result.Failure(PersistenceErrors.UserInGroup.AlreadyExists);
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            Result obj;
            obj = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId &&
                    uig.RoleId == entity.RoleId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            return obj;
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            Result obj;
            obj = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId &&
                    uig.RoleId == entity.RoleId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            return obj;
        }
    }
}
