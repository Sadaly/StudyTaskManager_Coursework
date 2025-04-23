using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetByGroupChatId
{
	internal class GroupChatMessageGetByGroupChatIdQueryHandler : IQueryHandler<GroupChatMessageGetByGroupChatIdQuery, List<GroupChatMessageResponse>>
	{
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;

		public GroupChatMessageGetByGroupChatIdQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
		{
            _groupChatMessageRepository = groupChatMessageRepository;
		}

		public async Task<Result<List<GroupChatMessageResponse>>> Handle(GroupChatMessageGetByGroupChatIdQuery request, CancellationToken cancellationToken)
		{
			var gcmResult = await _groupChatMessageRepository.GetMessagesByGroupChatIdAsync(request.StartIndex, request.Count, request.GroupChatId, cancellationToken);
			if (gcmResult.IsFailure) return Result.Failure<List<GroupChatMessageResponse>>(gcmResult);

            var listRes = gcmResult.Value.Select(u => new GroupChatMessageResponse(u)).ToList();

            return listRes;
		}
	}
}
