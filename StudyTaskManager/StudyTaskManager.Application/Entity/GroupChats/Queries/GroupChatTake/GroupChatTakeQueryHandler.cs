using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Groups.Queries;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatTake
{
    public sealed class GroupChatTakeQueryHandler : IQueryHandler<GroupChatTakeQuery, List<GroupChatResponse>>
    {
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatTakeQueryHandler(IGroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result<List<GroupChatResponse>>> Handle(GroupChatTakeQuery request, CancellationToken cancellationToken)
        {
            var chats = request.Perdicate == null
                ? await _groupChatRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _groupChatRepository.GetAllAsync(request.StartIndex, request.Count, request.Perdicate, cancellationToken);

            if (chats.IsFailure) return Result.Failure<List<GroupChatResponse>>(chats);

            return chats.Value.Select(c => new GroupChatResponse(c)).ToList();
        }
    }
}
