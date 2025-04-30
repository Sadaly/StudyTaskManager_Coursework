using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries.GroupChatParticipantTake
{
    public sealed class GroupChatParticipantTakeQueryHandler : IQueryHandler<GroupChatParticipantTakeQuery, List<GroupChatParticipantResponse>>
    {
        private readonly IGroupChatParticipantRepository _groupChatParticipantRepository;

        public GroupChatParticipantTakeQueryHandler(IGroupChatParticipantRepository groupChatParticipantRepository)
        {
            _groupChatParticipantRepository = groupChatParticipantRepository;
        }

        public async Task<Result<List<GroupChatParticipantResponse>>> Handle(GroupChatParticipantTakeQuery request, CancellationToken cancellationToken)
        {
            var participants = request.Perdicate == null
                ? await _groupChatParticipantRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _groupChatParticipantRepository.GetAllAsync(request.StartIndex, request.Count, request.Perdicate, cancellationToken);

            if (participants.IsFailure) return Result.Failure<List<GroupChatParticipantResponse>>(participants);

            return participants.Value.Select(p => new GroupChatParticipantResponse(p)).ToList();
        }
    }
}
