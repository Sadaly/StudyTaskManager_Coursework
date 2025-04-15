using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatMessageRepository : Generic.IRepository<GroupChatMessage>
    {
		Task<Result<GroupChatMessage>> GetMessageAsync(Guid groupChatId, ulong ordinal, CancellationToken cancellationToken);
	}
}
