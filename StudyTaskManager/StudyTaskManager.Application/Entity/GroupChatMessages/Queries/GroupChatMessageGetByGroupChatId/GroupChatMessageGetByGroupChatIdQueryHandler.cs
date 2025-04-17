using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId
{
	public class GroupChatMessageGetByGroupChatIdQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdQuery, List<GroupChatMessage>>
	{
		private readonly IGroupChatMessageRepository _gcm;

		public GroupChatMessageGetByGroupChatIdQueryHandler(IGroupChatMessageRepository gcm)
		{
			_gcm = gcm;
		}

		public async Task<Result<List<GroupChatMessage>>> Handle(GroupChatMessageGetByGroupChatIdQuery request, CancellationToken cancellationToken)
		{
			return await _gcm.GetMessagesByGroupChatIdAsync(request.GroupChatId, cancellationToken).ConfigureAwait(false);
		}
	}
}
