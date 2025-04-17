using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using System.Net.Mail;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupSendInvite
{
    class GroupSendInviteCommandHandler : ICommandHandler<GroupSendInviteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupInviteRepository _groupInviteRepository;

        public GroupSendInviteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupRepository groupRepository, IGroupInviteRepository groupInviteRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupInviteRepository = groupInviteRepository;
        }

        public async Task<Result> Handle(GroupSendInviteCommand request, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetByIdAsync(request.SenderId, cancellationToken);
            if (sender.IsFailure) return Result.Failure(sender);

            var receiver = await _userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);
            if (receiver.IsFailure) return Result.Failure(receiver);

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure(group);

            var invite = GroupInvite.Create(sender.Value, receiver.Value, group.Value);
            if (invite.IsFailure) return Result.Failure(invite);

            var add = await _groupInviteRepository.AddAsync(invite.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
