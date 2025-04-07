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

        public async Task<Result<BlockedUserInfo?>> GetByUser(User user, CancellationToken cancellationToken = default)
        {
            return await GetByUser(user.Id, cancellationToken);
        }

        public async Task<Result<BlockedUserInfo?>> GetByUser(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty) return Result.Failure<BlockedUserInfo?>(PersistenceErrors.User.IdEmpty);
            BlockedUserInfo? res = await _dbContext.Set<BlockedUserInfo>().FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            return Result.Success(res);
        }

        public override async Task<Result> AddAsync(BlockedUserInfo blockedUserInfo, CancellationToken cancellationToken = default)
        {
            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == blockedUserInfo.UserId, cancellationToken);
            if (user == null) return Result.Failure(PersistenceErrors.User.NotFound);

            await _dbContext.Set<BlockedUserInfo>().AddAsync(blockedUserInfo, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
