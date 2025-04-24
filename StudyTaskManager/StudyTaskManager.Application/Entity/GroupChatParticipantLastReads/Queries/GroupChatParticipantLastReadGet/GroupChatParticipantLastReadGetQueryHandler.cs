using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries.GroupChatParticipantLastReadGetByOrdinalAndGroupChatAndUserIds
{
    internal class GroupChatParticipantLastReadGetQueryHandler : IQueryHandler<GroupChatParticipantLastReadGetQuery, GroupChatParticipantLastReadResponse>
    {
        IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;
        IUserRepository _userRepository;
        IGroupChatRepository _groupChatRepository;

		public GroupChatParticipantLastReadGetQueryHandler(IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository, IUserRepository userRepository, IGroupChatRepository groupChatRepository)
		{
			_groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
			_userRepository = userRepository;
			_groupChatRepository = groupChatRepository;
		}

		public async Task<Result<GroupChatParticipantLastReadResponse>> Handle(GroupChatParticipantLastReadGetQuery request, CancellationToken cancellationToken)
		{
			var result = await _groupChatParticipantLastReadRepository.GetParticipantLastReadAsync(request.UserId, request.GroupChatId, cancellationToken);
			if (result.IsFailure) return Result.Failure<GroupChatParticipantLastReadResponse>(result);


			return new GroupChatParticipantLastReadResponse(result.Value);
		}
	}
}
