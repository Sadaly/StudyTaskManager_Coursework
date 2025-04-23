using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatParticipantLastReadRepository : Generic.IRepository<GroupChatParticipantLastRead>
    {

        /// <summary>
        /// Получить последнее прочитанное сообщение по пользователю, группе и порядковому номеру.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, который оставил сообщение.</param>
        /// <param name="groupChatId">Идентификатор группового чата, в котором оставлено сообщение.</param>
        /// <param name="lastReadMessageId">Порядковый номер сообщения в чате.</param>
        Task<Result<GroupChatParticipantLastRead>> GetParticipantLastReadAsync(Guid userId, Guid groupChatId, ulong lastReadMessageId, CancellationToken cancellationToken);
    }
}
