using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageDelete
{
	public class GroupChatMessageDeleteCommandHandler : ICommandHandler<GroupChatMessageDeleteCommand>
	{
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GroupChatMessageDeleteCommandHandler(IUnitOfWork unitOfWork, IGroupChatMessageRepository groupChatMessageRepository)
		{
			_unitOfWork = unitOfWork;
			_groupChatMessageRepository = groupChatMessageRepository;
		}

		async Task<Result> IRequestHandler<GroupChatMessageDeleteCommand, Result>.Handle(GroupChatMessageDeleteCommand request, CancellationToken cancellationToken)
		{
			var groupChatId = request.GroupChatId;
			var ordinal = request.Ordinal;

			var gcmResult = _groupChatMessageRepository.GetMessageAsync(groupChatId, ordinal, cancellationToken).Result;

			//Возвращаем ошибку если элемент не найден
			if (gcmResult.IsFailure) return Result.Failure<(Guid, ulong)>(gcmResult.Error);


			var remove = await _groupChatMessageRepository.RemoveAsync(gcmResult.Value);
			if (remove.IsFailure) return remove;

			await _unitOfWork.SaveChangesAsync();

			return remove;
		}
	}
}
