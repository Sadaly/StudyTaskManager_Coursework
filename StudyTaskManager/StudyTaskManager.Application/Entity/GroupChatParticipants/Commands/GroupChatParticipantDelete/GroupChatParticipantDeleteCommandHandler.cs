using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantDelete
{
	internal class GroupChatParticipantDeleteCommandHandler : ICommandHandler<GroupChatParticipantDeleteCommand>
	{
		IGroupChatParticipantRepository _groupChatParticipantRepository;
		IUnitOfWork	_unitOfWork;

		public GroupChatParticipantDeleteCommandHandler(IGroupChatParticipantRepository groupChatParticipantRepository, IUnitOfWork unitOfWork)
		{
			_groupChatParticipantRepository = groupChatParticipantRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(GroupChatParticipantDeleteCommand request, CancellationToken cancellationToken)
		{
			var gcpRes = await _groupChatParticipantRepository.Get(request.UserId, request.GroupChatId, cancellationToken);
			if (gcpRes.IsFailure) return Result.Failure(PersistenceErrors.GroupChatParticipant.NotFound);

			await _groupChatParticipantRepository.RemoveAsync(gcpRes.Value, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
