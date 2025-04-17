using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatIdOrdinal
{
	public class GroupChatMessageGetByGroupChatIdOrdinalQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdOrdinalQuery, GroupChatMessage>
	{
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;

		public GroupChatMessageGetByGroupChatIdOrdinalQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
		{
			_groupChatMessageRepository = groupChatMessageRepository;
		}

		public async Task<Result<GroupChatMessage>> Handle(GroupChatMessageGetByGroupChatIdOrdinalQuery request, CancellationToken cancellationToken)
		{
			return await _groupChatMessageRepository.GetMessageAsync(request.GroupChatId, request.Ordinal, cancellationToken);
		}
	}
}
