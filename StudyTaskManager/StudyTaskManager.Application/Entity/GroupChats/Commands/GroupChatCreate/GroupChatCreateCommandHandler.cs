using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;
using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChats.Commands.GroupChatCreate
{
    public sealed class GroupChatCreateCommandHandler : ICommandHandler<GroupChatCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupChatRepository _groupChatRepository;

        public GroupChatCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupChatRepository groupChatRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupChatRepository = groupChatRepository;
        }

        public async Task<Result<Guid>> Handle(GroupChatCreateCommand request, CancellationToken cancellationToken)
        {
            var name = Title.Create(request.Name);
            if (name.IsFailure) return Result.Failure<Guid>(name);

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<Guid>(group);

            var chat = GroupChat.Create(group.Value, name.Value, request.IsPublic);
            if (chat.IsFailure) return Result.Failure<Guid>(chat);

            var add = await _groupChatRepository.AddAsync(chat.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(chat.Value.Id);
        }
    }
}
