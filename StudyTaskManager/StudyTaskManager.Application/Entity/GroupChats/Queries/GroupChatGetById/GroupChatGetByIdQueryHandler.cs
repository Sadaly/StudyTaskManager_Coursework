using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries.GroupChatGetById
{
    public sealed class GroupChatGetByIdQueryHandler : IQueryHandler<GroupChatGetByIdQuery, GroupChatResponse>
    {
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatGetByIdQueryHandler(IGroupChatRepository groupChatRepository)
        {
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result<GroupChatResponse>> Handle(GroupChatGetByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _groupChatRepository.GetByIdAsync(request.Id, cancellationToken);
            if (chat.IsFailure) return Result.Failure<GroupChatResponse>(chat);

            return new GroupChatResponse(chat.Value);
        }
    }
}
