using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetAll
{
    public sealed class GroupChatGetAllQueryHandler : IQueryHandler<GroupChatGetAllQuery, List<GroupChatResponse>>
    {
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatGetAllQueryHandler(IGroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result<List<GroupChatResponse>>> Handle(GroupChatGetAllQuery request, CancellationToken cancellationToken)
        {
            var chats = request.Predicate == null
                ? await _groupChatRepository.GetAllAsync(cancellationToken)
                : await _groupChatRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (chats.IsFailure) return Result.Failure<List<GroupChatResponse>>(chats);

            return chats.Value.Select(c => new GroupChatResponse(c)).ToList();
        }
    }
}
