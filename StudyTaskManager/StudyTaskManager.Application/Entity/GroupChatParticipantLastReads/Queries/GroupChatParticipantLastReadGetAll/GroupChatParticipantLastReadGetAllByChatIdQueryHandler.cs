using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadGetAll
{
	internal class GroupChatParticipantLastReadGetAllQueryHandler : IQueryHandler<GroupChatParticipantLastReadGetAllQuery, List<GroupChatParticipantLastReadResponse>>
	{
		IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;

		public GroupChatParticipantLastReadGetAllQueryHandler(IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository)
		{
			_groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
		}

		public async Task<Result<List<GroupChatParticipantLastReadResponse>>> Handle(GroupChatParticipantLastReadGetAllQuery request, CancellationToken cancellationToken)
		{
			var gcplr = request.Predicate == null
                ? await _groupChatParticipantLastReadRepository.GetAllAsync(cancellationToken)
                : await _groupChatParticipantLastReadRepository.GetAllAsync(request.Predicate, cancellationToken);

            if (gcplr.IsFailure) return Result.Failure<List<GroupChatParticipantLastReadResponse>>(gcplr.Error);

            var listRes = gcplr.Value.Select(u => new GroupChatParticipantLastReadResponse(u)).ToList();

            return listRes;
		}
	}
}
