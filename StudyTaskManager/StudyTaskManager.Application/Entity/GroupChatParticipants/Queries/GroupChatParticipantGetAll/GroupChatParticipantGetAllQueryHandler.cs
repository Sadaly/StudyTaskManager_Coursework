using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantGetAll
{
    public sealed class GroupChatParticipantGetAllQueryHandler : IQueryHandler<GroupChatParticipantGetAllQuery, List<GroupChatParticipantResponse>>
    {
        private readonly IGroupChatParticipantRepository _groupChatParticipantRepository;

        public GroupChatParticipantGetAllQueryHandler(IGroupChatParticipantRepository groupChatParticipantRepository)
        {
            _groupChatParticipantRepository = groupChatParticipantRepository;
        }

        public async Task<Result<List<GroupChatParticipantResponse>>> Handle(GroupChatParticipantGetAllQuery request, CancellationToken cancellationToken)
        {
            var participants = request.Predicate == null
                ? await _groupChatParticipantRepository.GetAllAsync(cancellationToken)
                : await _groupChatParticipantRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (participants.IsFailure) return Result.Failure<List<GroupChatParticipantResponse>>(participants);

            return participants.Value.Select(p => new GroupChatParticipantResponse(p)).ToList();
        }
    }
}
