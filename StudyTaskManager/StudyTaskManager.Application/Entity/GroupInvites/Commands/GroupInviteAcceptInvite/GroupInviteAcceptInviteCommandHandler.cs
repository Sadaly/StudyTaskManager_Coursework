using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.DomainEvents;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.GroupInvites.Commands.GroupInviteAcceptInvite
{
    public class GroupInviteAcceptInviteCommandHandler : ICommandHandler<GroupInviteAcceptInviteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupInviteRepository _groupInviteRepository;

        public GroupInviteAcceptInviteCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IGroupRepository groupRepository, IGroupInviteRepository groupInviteRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _groupInviteRepository = groupInviteRepository;
        }

        public async Task<Result> Handle(GroupInviteAcceptInviteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.ReceiverId, cancellationToken);
            if (user.IsFailure) return user;

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return group;

            var invite = await _groupInviteRepository.GetByUserAndGropu(user.Value, group.Value, cancellationToken);
            if (invite.IsFailure) return invite;

            var resAccept = invite.Value.AcceptInvite();
            if (resAccept.IsFailure) return resAccept;

            var updateDB = await _groupInviteRepository.UpdateAsync(invite.Value, cancellationToken);
            if (updateDB.IsFailure) return updateDB;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
