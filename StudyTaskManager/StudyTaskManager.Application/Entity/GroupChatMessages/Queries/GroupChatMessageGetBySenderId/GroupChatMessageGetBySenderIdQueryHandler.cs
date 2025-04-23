using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetBySenderId
{
    internal sealed class GroupChatMessageGetBySenderIdQueryHandler : IQueryHandler<GroupChatMessageGetBySenderIdQuery, List<GroupChatMessageResponse>>
    {
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatMessageGetBySenderIdQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
        {
            _groupChatMessageRepository = groupChatMessageRepository;
        }

        public async Task<Result<List<GroupChatMessageResponse>>> Handle(GroupChatMessageGetBySenderIdQuery request, CancellationToken cancellationToken)
        {
            var gcmResult = await _groupChatMessageRepository.GetMessagesBySenderIdAsync(request.StartIndex, request.Count, request.SenderId, cancellationToken);
            if (gcmResult.IsFailure) return Result.Failure<List<GroupChatMessageResponse>>(gcmResult);

            var listRes = gcmResult.Value.Select(u => new GroupChatMessageResponse(u)).ToList();

            return listRes;
        }
    }
}
