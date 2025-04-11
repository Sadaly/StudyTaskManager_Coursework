using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class BlockedUserInfoRepository : Generic.TRepository<BlockedUserInfo>, IBlockedUserInfoRepository
    {
        public BlockedUserInfoRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<BlockedUserInfo>> GetByUser(User user, CancellationToken cancellationToken = default)
        {
            return await GetByUser(user.Id, cancellationToken);
        }

        public async Task<Result<BlockedUserInfo>> GetByUser(Guid userId, CancellationToken cancellationToken = default)
        {
            Result<User> user = await GetFromDBAsync<User>(userId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return Result.Failure<BlockedUserInfo>(user.Error); }

            return await GetFromDBAsync(bui => bui.UserId == userId, PersistenceErrors.BlockedUserInfo.NotFound, cancellationToken);
        }

        protected override async Task<Result> VerificationBeforeAddingAsync(BlockedUserInfo entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return user; }

            Result<BlockedUserInfo> blockedUserInfo = await GetFromDBAsync(
                bui => bui.UserId == entity.UserId,
                PersistenceErrors.BlockedUserInfo.NotFound,
                cancellationToken);
            if (blockedUserInfo.IsFailure) { return Result.Success(); }
            return Result.Failure(PersistenceErrors.BlockedUserInfo.AlreadyExist);
        }

        protected override async Task<Result> VerificationBeforeUpdateAsync(BlockedUserInfo entity, CancellationToken cancellationToken)
        {
            Result<User> user = await GetFromDBAsync<User>(entity.UserId, PersistenceErrors.User.IdEmpty, PersistenceErrors.User.NotFound, cancellationToken);
            if (user.IsFailure) { return user; }

            Result<BlockedUserInfo> blockedUserInfo = await GetFromDBAsync(bui => bui.UserId == entity.UserId, PersistenceErrors.BlockedUserInfo.NotFound, cancellationToken);
            if (blockedUserInfo.IsFailure) { return blockedUserInfo; }
            return Result.Success();
        }

        protected override async Task<Result> VerificationBeforeRemoveAsync(BlockedUserInfo entity, CancellationToken cancellationToken)
        {
            Result<BlockedUserInfo> blockedUserInfo = await GetFromDBAsync(bui => bui.UserId == entity.UserId, PersistenceErrors.BlockedUserInfo.NotFound, cancellationToken);
            if (blockedUserInfo.IsFailure) { return blockedUserInfo; }
            return Result.Success();
        }
    }
}
