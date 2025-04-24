using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Commands.GroupChatParticipantCreate
{
	internal class GroupChatParticipantCreateCommandHandler : ICommandHandler<GroupChatParticipantCreateCommand>
	{
		IUnitOfWork _unitOfWork;
		IGroupChatParticipantRepository _groupChatParticipantRepository;
		IGroupChatRepository _groupChatRepository;
		IUserRepository _userRepository;

		public GroupChatParticipantCreateCommandHandler(IUnitOfWork unitOfWork, IGroupChatParticipantRepository groupChatParticipantRepository, IGroupChatRepository groupChatRepository, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_groupChatParticipantRepository = groupChatParticipantRepository;
			_groupChatRepository = groupChatRepository;
			_userRepository = userRepository;
		}

		public async Task<Result> Handle(GroupChatParticipantCreateCommand request, CancellationToken cancellationToken)
		{
			var userRes = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (userRes.IsFailure) return Result.Failure(userRes);

			var gcRes = await _groupChatRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (gcRes.IsFailure) return Result.Failure(gcRes);

			var alreadyExist = await _groupChatParticipantRepository.Get(request.UserId, request.GroupChatId, cancellationToken);
			if (alreadyExist.IsSuccess) return Result.Failure(PersistenceErrors.GroupChatParticipant.AlreadyExist);

			var result = GroupChatParticipant.Create(userRes.Value, gcRes.Value);

			await _groupChatParticipantRepository.AddAsync(result.Value, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
