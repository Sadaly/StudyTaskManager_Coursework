using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Errors;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Commands.GroupChatParticipantLastReadUpdate
{
    internal class GroupChatParticipantLastReadUpdateCommandHandler : ICommandHandler<GroupChatParticipantLastReadUpdateCommand>
	{
		IGroupChatMessageRepository _groupChatMessageRepository;
		IGroupChatParticipantLastReadRepository _groupChatParticipantLastReadRepository;
        IUnitOfWork _unitOfWork;

		public GroupChatParticipantLastReadUpdateCommandHandler(IGroupChatMessageRepository groupChatMessageRepository, IGroupChatParticipantLastReadRepository groupChatParticipantLastReadRepository, IUnitOfWork unitOfWork)
		{
			_groupChatMessageRepository = groupChatMessageRepository;
			_groupChatParticipantLastReadRepository = groupChatParticipantLastReadRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(GroupChatParticipantLastReadUpdateCommand request, CancellationToken cancellationToken)
		{
			var gcmRes = await _groupChatMessageRepository.GetMessageAsync(request.GroupChatId, request.NewLastReadId, cancellationToken);
			if (gcmRes.IsFailure) return Result.Failure(gcmRes);

			var result = await _groupChatParticipantLastReadRepository.GetParticipantLastReadAsync(request.UserId, request.GroupChatId, cancellationToken);
            if (result.IsFailure) return Result.Failure(result);
			if (request.NewLastReadId == result.Value.LastReadMessageId) return Result.Failure(PersistenceErrors.GroupChatParticipantLastRead.UpdatedSameValue);

			result.Value.UpdateReadMessage(gcmRes.Value);
			if (result.IsFailure) return Result.Failure(result);

			await _groupChatParticipantLastReadRepository.UpdateAsync(result.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
