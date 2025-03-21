using Microsoft.EntityFrameworkCore;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Persistence.DB;

namespace StudyTaskManager.Persistence.Repository
{
    class BlockedUserInfoRepository : Generic.TRepository<BlockedUserInfo>, IBlockedUserInfoRepository
    {
        public BlockedUserInfoRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<BlockedUserInfo?> GetByUser(User user, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<BlockedUserInfo>().FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
        }
    }
}
