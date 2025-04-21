using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteDelete
{
    public class GroupInviteDeleteCommandHandler : ICommandHandler<GroupInviteDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupInviteRepository _groupInviteRepository;

        public GroupInviteDeleteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupRepository groupRepository, IGroupInviteRepository groupInviteRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupInviteRepository = groupInviteRepository;
        }

        public async Task<Result> Handle(GroupInviteDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);
            if (user.IsFailure) return user;

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return group;

            var invite = await _groupInviteRepository.GetByUserAndGropu(user.Value, group.Value, cancellationToken);
            if (invite.IsFailure) return invite;

            var remove = await _groupInviteRepository.RemoveAsync(invite.Value, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
