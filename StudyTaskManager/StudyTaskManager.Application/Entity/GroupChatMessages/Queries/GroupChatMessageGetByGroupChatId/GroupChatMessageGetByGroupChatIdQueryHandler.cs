using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId
{
	internal class GroupChatMessageGetByGroupChatIdQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdQuery, GroupChatMessageListResponse>
	{
		private readonly IGroupChatMessageRepository _gcm;

		public GroupChatMessageGetByGroupChatIdQueryHandler(IGroupChatMessageRepository gcm)
		{
			_gcm = gcm;
		}

		public async Task<Result<GroupChatMessageListResponse>> Handle(GroupChatMessageGetByGroupChatIdQuery request, CancellationToken cancellationToken)
		{
			var gcmResult = await _gcm.GetMessagesByGroupChatIdAsync(request.GroupChatId, cancellationToken);
			if (gcmResult.IsFailure) return Result.Failure<GroupChatMessageListResponse>(gcmResult);

            return new GroupChatMessageListResponse(gcmResult.Value);
		}
	}
}
