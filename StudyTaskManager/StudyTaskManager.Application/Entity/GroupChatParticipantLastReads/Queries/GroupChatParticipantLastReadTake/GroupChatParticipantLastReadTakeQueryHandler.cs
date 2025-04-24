using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadTake
{
    internal class GroupChatParticipantLastReadTakeQueryHandler : IQueryHandler<GroupChatParticipantLastReadTakeQuery, List<GroupChatParticipantLastReadResponse>>
	{
		IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;

		public GroupChatParticipantLastReadTakeQueryHandler(IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository)
		{
			_groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
		}

		public async Task<Result<List<GroupChatParticipantLastReadResponse>>> Handle(GroupChatParticipantLastReadTakeQuery request, CancellationToken cancellationToken)
		{
			var gcplr = request.Predicate == null
				? await _groupChatParticipantLastReadRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
				: await _groupChatParticipantLastReadRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

			if (gcplr.IsFailure) return Result.Failure<List<GroupChatParticipantLastReadResponse>>(gcplr.Error);

			var listRes = gcplr.Value.Select(u => new GroupChatParticipantLastReadResponse(u)).ToList();

			return listRes;
		}
	}
}
