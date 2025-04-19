using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    public class UserInGroupRepository : Generic.TRepository<UserInGroup>, IUserInGroupRepository
    {
        public UserInGroupRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<UserInGroup>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<Result<List<UserInGroup>>> GetByGroupAsync(int startIndex, int count, Group group, CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(
                startIndex,
                count,
                uig => uig.GroupId == group.Id,
                cancellationToken);
        }

        public async Task<Result<List<UserInGroup>>> GetByUserAsync(User user, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UserInGroup>()
                .Where(uig => uig.UserId == user.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<Result<List<UserInGroup>>> GetByUserAsync(int startIndex, int count, User user, CancellationToken cancellationToken = default)
        {
            return await GetFromDBWhereAsync(
                startIndex,
                count,
                uig => uig.UserId == user.Id,
                cancellationToken);
        }

        public async Task<Result<UserInGroup>> GetByUserAndGroupAsync(User user, Group group, CancellationToken cancellationToken = default)
        {
            return await GetByUserAndGroupAsync(user.Id, group.Id, cancellationToken);
        }
        public async Task<Result<UserInGroup>> GetByUserAndGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken = default)
        {
            var user = await GetFromDBAsync<User>(userId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) return Result.Failure<UserInGroup>(user.Error);

            var group = await GetFromDBAsync<Group>(groupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) return Result.Failure<UserInGroup>(user.Error);

            return await GetFromDBAsync(
                uig =>
                    uig.UserId == userId &&
                    uig.GroupId == groupId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
        }

        #region verification
        protected override async Task<Result> VerificationBeforeAddingAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            var user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return user; }

            var group = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) { return group; }

            var groupRole = await GetFromDBAsync<GroupRole>(entity.RoleId, PersistenceErrors.GroupRole.IdEmpty, PersistenceErrors.GroupRole.NotFound, cancellationToken);
            if (groupRole.IsFailure) { return groupRole; }

            var userInGroup = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            if (userInGroup.IsSuccess) { return Result.Failure(PersistenceErrors.UserInGroup.AlreadyExists); }
            return Result.Success();
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            var userInGroup = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            return userInGroup;
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(UserInGroup entity, CancellationToken cancellationToken)
        {
            var userInGrup = await GetFromDBAsync(
                uig =>
                    uig.UserId == entity.UserId &&
                    uig.GroupId == entity.GroupId
                , PersistenceErrors.UserInGroup.NotFound
                , cancellationToken);
            return userInGrup;
        }
        #endregion
    }
}