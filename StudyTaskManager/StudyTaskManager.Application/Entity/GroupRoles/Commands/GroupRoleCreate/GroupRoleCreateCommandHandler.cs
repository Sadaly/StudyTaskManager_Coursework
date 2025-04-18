using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.Group;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleCreate
{
    public class GroupRoleCreateCommandHandler : ICommandHandler<GroupRoleCreateCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRoleCreateCommandHandler(IUnitOfWork unitOfWork, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<Guid>> Handle(GroupRoleCreateCommand request, CancellationToken cancellationToken)
        {
            var roleName = Title.Create(request.RoleName);
            if (roleName.IsFailure) return Result.Failure<Guid>(roleName);

            var role = GroupRole.Create(
                roleName.Value,
                request.CanCreateTasks,
                request.CanManageRoles,
                request.CanCreateTaskUpdates,
                request.CanChangeTaskUpdates,
                request.CanInviteUsers,
                null);
            if (role.IsFailure) return Result.Failure<Guid>(role);

            var add = await _groupRoleRepository.AddAsync(role.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<Guid>(add);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(role.Value.Id);
        }
    }
}
