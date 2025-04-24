using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatParticipantLastReadRepository : Generic.IRepository<GroupChatParticipantLastRead>
    {

        /// <summary>
        /// Получить последнее прочитанное сообщение по пользователю и групповому чату.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, который оставил сообщение.</param>
        /// <param name="groupChatId">Идентификатор группового чата, в котором оставлено сообщение.</param>
        Task<Result<GroupChatParticipantLastRead>> GetParticipantLastReadAsync(Guid userId, Guid groupChatId, CancellationToken cancellationToken);
    }
}
