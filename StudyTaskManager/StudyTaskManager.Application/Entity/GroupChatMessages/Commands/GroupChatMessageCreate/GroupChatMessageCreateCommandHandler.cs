using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group.Chat;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Commands.GroupChatMessageCreate
{
    internal class GroupChatMessageCreateCommandHandler : ICommandHandler<GroupChatMessageCreateCommand, (Guid, ulong)>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupChatRepository _groupChatRepository;
		private readonly IGroupChatMessageRepository _groupChatMessageRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GroupChatMessageCreateCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupChatRepository groupChatRepository, IGroupChatMessageRepository groupChatMessageRepository)
		{
			_userRepository = userRepository;
			_groupChatRepository = groupChatRepository;
			_unitOfWork = unitOfWork;
			_groupChatMessageRepository = groupChatMessageRepository;
		}   

		async Task<Result<(Guid, ulong)>> IRequestHandler<GroupChatMessageCreateCommand, Result<(Guid, ulong)>>.Handle(GroupChatMessageCreateCommand request, CancellationToken cancellationToken)
        {
            var senderId = request.SenderId;
            var groupChatId = request.GroupChatId;
            var ordinal = request.Ordinal;

			var contentResult = Content.Create(request.Content);

			//Возвращаем ошибку если элемент не создан
			if (contentResult.IsFailure) return Result.Failure<(Guid, ulong)>(contentResult.Error);

			var userResult = await _userRepository.GetByIdAsync(senderId, cancellationToken);

			//Возвращаем ошибку если элемент не найден
			if (userResult.IsFailure) return Result.Failure<(Guid, ulong)>(userResult.Error);

			var groupChatResult = await _groupChatRepository.GetByIdAsync(senderId, cancellationToken);

            //Возвращаем ошибку если элемент не найден
			if (groupChatResult.IsFailure) return Result.Failure<(Guid, ulong)>(groupChatResult.Error);

			var gcmResult = GroupChatMessage.Create(
				groupChatResult.Value,
                ordinal,
                userResult.Value,
				contentResult.Value
				);

			//Возвращаем ошибку если элемент не найден
			if (gcmResult.IsFailure) return Result.Failure<(Guid, ulong)>(gcmResult.Error);


			await _groupChatMessageRepository.AddAsync(gcmResult.Value);
			await _unitOfWork.SaveChangesAsync();

			return Result.Success((groupChatId, ordinal));
        }
    }
}
