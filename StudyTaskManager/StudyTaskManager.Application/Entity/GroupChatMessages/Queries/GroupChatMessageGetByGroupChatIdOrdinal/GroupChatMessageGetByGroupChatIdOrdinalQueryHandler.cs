using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatIdOrdinal
{
	internal class GroupChatMessageGetByGroupChatIdOrdinalQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdOrdinalQuery, GroupChatMessageResponse>
	{
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;

		public GroupChatMessageGetByGroupChatIdOrdinalQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
		{
			_groupChatMessageRepository = groupChatMessageRepository;
		}

		public async Task<Result<GroupChatMessageResponse>> Handle(GroupChatMessageGetByGroupChatIdOrdinalQuery request, CancellationToken cancellationToken)
		{
			var gcmResult = await _groupChatMessageRepository.GetMessageAsync(request.GroupChatId, request.Ordinal, cancellationToken);
			if (gcmResult.IsFailure) return Result.Failure<GroupChatMessageResponse>(gcmResult);
            return new GroupChatMessageResponse(gcmResult.Value);
        }
	}
}
