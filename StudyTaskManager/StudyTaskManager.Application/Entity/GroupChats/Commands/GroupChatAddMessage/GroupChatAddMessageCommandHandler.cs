using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatAddMessage
{
    public sealed class GroupChatAddMessageCommandHandler : ICommandHandler<GroupChatAddMessageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupChatRepository _groupChatRepository;
        private readonly IGroupChatMessageRepository _groupChatMessageRepository;

        public GroupChatAddMessageCommandHandler(IUserRepository userRepository, IGroupChatRepository groupChatRepository, IGroupChatMessageRepository groupChatMessageRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _groupChatRepository = groupChatRepository;
            _groupChatMessageRepository = groupChatMessageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GroupChatAddMessageCommand request, CancellationToken cancellationToken)
        {
            var content = Content.Create(request.Content);
            if (content.IsFailure) return Result.Failure(content);

            var sender = await _userRepository.GetByIdAsync(request.SenderId, cancellationToken);
            if (sender.IsFailure) return Result.Failure(sender);

            var chat = await _groupChatRepository.GetByIdAsync(request.GroupChatId, cancellationToken);
            if (chat.IsFailure) return Result.Failure(chat);

            var message = GroupChatMessage.Create(chat.Value, request.Ordinal, sender.Value, content.Value);
            if (message.IsFailure) return Result.Failure(message);

            var add = await _groupChatMessageRepository.AddAsync(message.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}