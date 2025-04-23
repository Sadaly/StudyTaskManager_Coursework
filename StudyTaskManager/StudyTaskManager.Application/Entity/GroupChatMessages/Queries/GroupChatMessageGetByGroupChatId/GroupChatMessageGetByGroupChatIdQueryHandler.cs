using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId
{
	internal class GroupChatMessageGetByGroupChatIdQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdQuery, List<GroupChatMessageResponse>>
	{
		private readonly IGroupChatMessageRepository _gcm;

		public GroupChatMessageGetByGroupChatIdQueryHandler(IGroupChatMessageRepository gcm)
		{
			_gcm = gcm;
		}

		public async Task<Result<List<GroupChatMessageResponse>>> Handle(GroupChatMessageGetByGroupChatIdQuery request, CancellationToken cancellationToken)
		{
			var gcmResult = await _gcm.GetMessagesByGroupChatIdAsync(request.GroupChatId, cancellationToken);
			if (gcmResult.IsFailure) return Result.Failure<List<GroupChatMessageResponse>>(gcmResult);

            var listRes = gcmResult.Value.Select(u => new GroupChatMessageResponse(u)).ToList();

            return listRes;
		}
	}
}
