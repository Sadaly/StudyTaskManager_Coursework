using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll
{
    internal sealed class GroupChatMessageGetAllQueryHandler : IQueryHandler<GroupChatMessageGetAllQuery, GroupChatMessageResponse>
    {
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatMessageGetAllQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
        {
            _groupChatMessageRepository = groupChatMessageRepository;
        }

        public async Task<Result<GroupChatMessageResponse>> Handle(GroupChatMessageGetAllQuery request, CancellationToken cancellationToken)
        {
            var gcmResult = await _groupChatMessageRepository.GetAllAsync(cancellationToken);
            if (gcmResult.IsFailure) return Result.Failure<GroupChatMessageResponse>(gcmResult);
            return new GroupChatMessageResponse(gcmResult.Value);
        }
    }
}
