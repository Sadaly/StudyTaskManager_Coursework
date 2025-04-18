using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageUpdate
{
	public class GroupChatMessageUpdateCommandHandler : ICommandHandler<GroupChatMessageUpdateCommand, (Guid, ulong)>
	{
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GroupChatMessageUpdateCommandHandler(IUnitOfWork unitOfWork, IGroupChatMessageRepository groupChatMessageRepository)
		{
			_unitOfWork = unitOfWork;
			_groupChatMessageRepository = groupChatMessageRepository;
		}

		async Task<Result<(Guid, ulong)>> IRequestHandler<GroupChatMessageUpdateCommand, Result<(Guid, ulong)>>.Handle(GroupChatMessageUpdateCommand request, CancellationToken cancellationToken)
		{
			var groupChatId = request.GroupChatId;
			var ordinal = request.Ordinal;

			var contentResult = Content.Create(request.Content);

			//Возвращаем ошибку если элемент не создан
			if (contentResult.IsFailure) return Result.Failure<(Guid, ulong)>(contentResult.Error);

			var gcmResult = await _groupChatMessageRepository.GetMessageAsync(groupChatId, ordinal, cancellationToken);

			//Возвращаем ошибку если элемент не найден
			if (gcmResult.IsFailure) return Result.Failure<(Guid, ulong)>(gcmResult.Error);


			var update = await _groupChatMessageRepository.UpdateAsync(gcmResult.Value);
			if (update.IsFailure) return Result.Failure<(Guid, ulong)>(update.Error);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success((groupChatId, ordinal));
		}
	}
}
