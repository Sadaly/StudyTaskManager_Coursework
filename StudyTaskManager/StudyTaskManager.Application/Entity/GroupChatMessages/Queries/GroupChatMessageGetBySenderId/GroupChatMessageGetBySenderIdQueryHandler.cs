using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetBySenderId
{
    internal sealed class GroupChatMessageGetBySenderIdQueryHandler : IQueryHandler<GroupChatMessageGetBySenderIdQuery, GroupChatMessageListResponse>
    {
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatMessageGetBySenderIdQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
        {
            _groupChatMessageRepository = groupChatMessageRepository;
        }

        public async Task<Result<GroupChatMessageListResponse>> Handle(GroupChatMessageGetBySenderIdQuery request, CancellationToken cancellationToken)
        {
            var gcmResult = await _groupChatMessageRepository.GetMessagesBySenderIdAsync(request.SenderId, cancellationToken);
            if (gcmResult.IsFailure) return Result.Failure<GroupChatMessageListResponse>(gcmResult);
            return new GroupChatMessageListResponse(gcmResult.Value);
        }
    }
}
