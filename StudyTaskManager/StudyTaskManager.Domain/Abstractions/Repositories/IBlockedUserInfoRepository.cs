using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IBlockedUserInfoRepository : Generic.IRepository<BlockedUserInfo>
    {
        /// <summary>
        /// Выдать информацию о блокировке для юзера, если она есть.
        /// </summary>
        Task<BlockedUserInfo?> GetByUser(User user, CancellationToken cancellationToken = default);
    }
}
