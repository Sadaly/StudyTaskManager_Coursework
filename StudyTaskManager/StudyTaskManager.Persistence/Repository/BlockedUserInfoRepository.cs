using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Persistence.Repository
{
    class BlockedUserInfoRepository : Generic.TRepository<BlockedUserInfo>, IBlockedUserInfoRepository
    {
        public BlockedUserInfoRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Result<BlockedUserInfo?>> GetByUser(User user, CancellationToken cancellationToken = default)
        {
            BlockedUserInfo? res = await _dbContext.Set<BlockedUserInfo>().FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            return Result.Success(res);
        }
    }
}
