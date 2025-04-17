using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveRole
{
    class GroupRemoveRoleCommandHandler : ICommandHandler<GroupRemoveRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GroupRemoveRoleCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result> Handle(GroupRemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return group;

            var role = await _groupRoleRepository.GetByIdAsync(request.RoleId, cancellationToken);
            if (role.IsFailure) return role;

            if (role.Value.GroupId is null) return Result.Failure(CommandErrors.DeleteSharedRole);
            if (role.Value.GroupId != group.Value.Id) return Result.Failure(CommandErrors.DeleteFromAnotherGroup);

            var remove = await _groupRoleRepository.RemoveAsync(role.Value, cancellationToken);
            if (remove.IsFailure) return remove;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
