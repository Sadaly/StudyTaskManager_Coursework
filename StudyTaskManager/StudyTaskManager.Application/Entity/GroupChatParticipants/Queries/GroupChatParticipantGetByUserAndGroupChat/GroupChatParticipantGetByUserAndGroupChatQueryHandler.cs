using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetByUserAndGroupChat
{
    public sealed class GroupChatParticipantGetByUserAndGroupChatQueryHandler : IQueryHandler<GroupChatParticipantGetByUserAndGroupChatQuery, GroupChatParticipantResponse>
    {
        private readonly IGroupChatParticipantRepository _groupChatParticipantRepository;

        public GroupChatParticipantGetByUserAndGroupChatQueryHandler(IGroupChatParticipantRepository groupChatParticipantRepository)
        {
            _groupChatParticipantRepository = groupChatParticipantRepository;
        }

        public async Task<Result<GroupChatParticipantResponse>> Handle(GroupChatParticipantGetByUserAndGroupChatQuery request, CancellationToken cancellationToken)
        {
            var participant = await _groupChatParticipantRepository.Get(request.UserId, request.GroupChatId, cancellationToken);
            if (participant.IsFailure) return Result.Failure<GroupChatParticipantResponse>(participant);

            return new GroupChatParticipantResponse(participant.Value);
        }
    }
}
