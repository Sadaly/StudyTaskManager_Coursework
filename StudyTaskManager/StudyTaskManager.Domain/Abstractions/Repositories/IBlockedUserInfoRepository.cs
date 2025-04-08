using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IBlockedUserInfoRepository : Generic.IRepository<BlockedUserInfo>
    {
        /// <summary>
        /// Выдать информацию о блокировке для юзера, если она есть, иначе ошибку.
        /// </summary>
        Task<Result<BlockedUserInfo>> GetByUser(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Выдать информацию о блокировке по id польозвателя, если она есть, иначе ошибку.
        /// </summary>
        Task<Result<BlockedUserInfo>> GetByUser(Guid userId, CancellationToken cancellationToken = default);
    }
}
