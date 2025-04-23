using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries.GroupChatMessageGetAll
{
    internal sealed class GroupChatMessageGetAllQueryHandler : IQueryHandler<GroupChatMessageGetAllQuery, List<GroupChatMessageResponse>>
    {
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatMessageGetAllQueryHandler(IGroupChatMessageRepository groupChatMessageRepository)
        {
            _groupChatMessageRepository = groupChatMessageRepository;
        }

        public async Task<Result<List<GroupChatMessageResponse>>> Handle(GroupChatMessageGetAllQuery request, CancellationToken cancellationToken)
        {
            var gcm = request.Predicate == null
                ? await _groupChatMessageRepository.GetAllAsync(cancellationToken)
                : await _groupChatMessageRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (gcm.IsFailure) return Result.Failure<List<GroupChatMessageResponse>>(gcm.Error);

            var listRes = gcm.Value.Select(u => new GroupChatMessageResponse(u)).ToList();

            return listRes;
        }
    }
}
