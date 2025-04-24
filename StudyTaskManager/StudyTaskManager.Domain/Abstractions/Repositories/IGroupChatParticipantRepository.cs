using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatParticipantRepository : Generic.IRepository<GroupChatParticipant>
    {
        public Task<Result<GroupChatParticipant>> Get(Guid UserId, Guid GroupChatId, CancellationToken cancellationToken);
    }
}
