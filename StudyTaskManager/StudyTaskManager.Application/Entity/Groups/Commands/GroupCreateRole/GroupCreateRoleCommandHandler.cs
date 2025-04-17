using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupCreateRole
{
    class GroupCreateRoleCommandHandler : ICommandHandler<GroupCreateRoleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupCreateRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<Guid>> Handle(GroupCreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleName = Title.Create(request.RoleName);
            if (roleName.IsFailure) return Result.Failure<Guid>(roleName);

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<Guid>(group);

            var role = GroupRole.Create(
                roleName.Value,
                request.CanCreateTasks,
                request.CanManageRoles,
                request.CanCreateTaskUpdates,
                request.CanChangeTaskUpdates,
                request.CanInviteUsers,
                group.Value);
            if (role.IsFailure) return Result.Failure<Guid>(role);

            var add = await _groupRoleRepository.AddAsync(role.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(role.Value.Id);
        }
    }
}
