using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.Group.Chat;

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

			var gcmResult = _groupChatMessageRepository.GetMessageAsync(groupChatId, ordinal, cancellationToken).Result;

			//Возвращаем ошибку если элемент не найден
			if (gcmResult.IsFailure) return Result.Failure<(Guid, ulong)>(gcmResult.Error);


			var update = await _groupChatMessageRepository.UpdateAsync(gcmResult.Value);
			if (update.IsFailure) return Result.Failure<(Guid, ulong)>(update.Error);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success((groupChatId, ordinal));
		}
	}
}
