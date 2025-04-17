using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Domain.Abstractions.Repositories
{
    public interface IGroupChatMessageRepository : Generic.IRepository<GroupChatMessage>
    {
		Task<Result<GroupChatMessage>> GetMessageAsync(Guid groupChatId, ulong ordinal, CancellationToken cancellationToken);
		Task<Result<List<GroupChatMessage>>> GetMessagesByGroupChatIdAsync(Guid groupChatId, CancellationToken cancellationToken);
		Task<Result<List<GroupChatMessage>>> GetMessagesBySenderIdAsync(Guid groupChatId, CancellationToken cancellationToken);
	}
}
