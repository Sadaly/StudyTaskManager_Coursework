using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class GroupInviteRepository : Generic.TRepository<GroupInvite>, IGroupInviteRepository
    {
        public GroupInviteRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<List<GroupInvite>>> GetByGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.GroupId == group.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupInvite>>> GetForUserAsync(User receiver, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.ReceiverId == receiver.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Result<List<GroupInvite>>> GetFromUserAsync(User sender, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<GroupInvite>()
                .Where(gi => gi.SenderId == sender.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(GroupInvite entity, CancellationToken cancellationToken)
        {
            var sender = await GetFromDBAsync<User>(entity.SenderId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (sender.IsFailure) { return sender; }
            var receiver = await GetFromDBAsync<User>(entity.ReceiverId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (receiver.IsFailure) { return receiver; }
            var group = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) { return group; }

            Error notFound = PersistenceErrors.UserInGroup.NotFound;
            var userInGroup = await GetFromDBAsync<UserInGroup>(
                uig =>
                    uig.UserId == entity.ReceiverId &&
                    uig.GroupId == entity.GroupId
                , notFound
                , cancellationToken);
            if (userInGroup.IsSuccess) { return Result.Failure(PersistenceErrors.GroupInvite.UserIsAlreadyInTheGroup); }
            if (userInGroup.Error != notFound) { return userInGroup; }

            var groupInvite = await GetFromDBAsync(
                gi =>
                    gi.SenderId == entity.SenderId &&       // Возможно стоит переделать
                    gi.ReceiverId == entity.ReceiverId &&   // чтобы проверка была только на 
                    gi.GroupId == entity.GroupId            // получателя и группу, без отправителя
                , PersistenceErrors.GroupInvite.NotFound
                , cancellationToken);
            if (groupInvite.IsSuccess) { return Result.Failure(PersistenceErrors.GroupInvite.AlreadyExist); }
            return Result.Success();
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(GroupInvite entity, CancellationToken cancellationToken)
        {
            var sender = await GetFromDBAsync<User>(entity.SenderId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (sender.IsFailure) { return sender; }
            var receiver = await GetFromDBAsync<User>(entity.ReceiverId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (receiver.IsFailure) { return receiver; }
            var group = await GetFromDBAsync<Group>(entity.GroupId, PersistenceErrors.Group.IdEmpty, PersistenceErrors.Group.NotFound, cancellationToken);
            if (group.IsFailure) { return group; }

            //Error notFound = PersistenceErrors.UserInGroup.NotFound;
            //obj = await GetFromDBAsync<UserInGroup>(uig => uig.UserId == entity.ReceiverId && uig.GroupId == entity.GroupId, notFound, cancellationToken);
            //if (obj.IsFailure)
            //{
            //    if (obj.Error != notFound) return obj;
            //}
            //else
            //{
            //    return Result.Failure(PersistenceErrors.GroupInvite.UserIsAlreadyInTheGroup);
            //}

            var groupInvite = await GetFromDBAsync(
                gi =>
                    gi.SenderId == entity.SenderId &&
                    gi.ReceiverId == entity.ReceiverId &&
                    gi.GroupId == entity.GroupId
                , PersistenceErrors.GroupInvite.NotFound
                , cancellationToken);
            return groupInvite;
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(GroupInvite entity, CancellationToken cancellationToken)
        {
            var groupInvite = await GetFromDBAsync(
                gi =>
                    gi.SenderId == entity.SenderId &&
                    gi.ReceiverId == entity.ReceiverId &&
                    gi.GroupId == entity.GroupId
                , PersistenceErrors.GroupInvite.NotFound
                , cancellationToken);
            return groupInvite;
        }
    }
}
