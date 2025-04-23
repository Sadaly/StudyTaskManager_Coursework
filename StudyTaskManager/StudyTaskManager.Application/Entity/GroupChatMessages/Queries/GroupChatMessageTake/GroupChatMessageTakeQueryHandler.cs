using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageTake
{
    internal class GroupChatMessageTakeQueryHandler : IQueryHandler<GroupChatMessageTakeQuery, List<GroupChatMessageResponse>>
    {
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatMessageTakeQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
        {
            _groupChatMessageRepository = groupChatMessageRepository;
        }

        public async Task<Result<List<GroupChatMessageResponse>>> Handle(GroupChatMessageTakeQuery request, CancellationToken cancellationToken)
        {
            var gcm = request.Predicate == null
                ? await _groupChatMessageRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _groupChatMessageRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

            if (gcm.IsFailure) return Result.Failure<List<GroupChatMessageResponse>>(gcm.Error);

            var listRes = gcm.Value.Select(u => new GroupChatMessageResponse(u)).ToList();

            return listRes;
        }
    }
}
