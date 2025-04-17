using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveInvite
{
    class GroupRemoveInviteCommandHandler : ICommandHandler<GroupRemoveInviteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupInviteRepository _groupInviteRepository;

        public GroupRemoveInviteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupRepository groupRepository, IGroupInviteRepository groupInviteRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupInviteRepository = groupInviteRepository;
        }

        public async Task<Result> Handle(GroupRemoveInviteCommand request, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetByIdAsync(request.SenderId, cancellationToken);
            if (sender.IsFailure) return Result.Failure(sender);

            var receiver = await _userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);
            if (receiver.IsFailure) return Result.Failure(receiver);

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure(group);

            var invite = GroupInvite.Create(sender.Value, receiver.Value, group.Value);
            if (invite.IsFailure) return Result.Failure(invite);

            var remove = await _groupInviteRepository.RemoveAsync(invite.Value, cancellationToken);
            if (remove.IsFailure) return Result.Failure(remove);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
