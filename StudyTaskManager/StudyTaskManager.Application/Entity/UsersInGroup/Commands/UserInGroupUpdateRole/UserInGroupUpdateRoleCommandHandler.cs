using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupUpdateRole;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete
{
    internal sealed class UserInGroupUpdateRoleCommandHandler : ICommandHandler<UserInGroupUpdateRoleCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;
        private readonly IGroupRepository _groupRepository;

        public UserInGroupUpdateRoleCommandHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository, IGroupRoleRepository groupRoleRepository, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
            _groupRoleRepository = groupRoleRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result> Handle(UserInGroupUpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return userInGroup;

            var newRole = await _groupRoleRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (newRole.IsFailure) return newRole;

            if (newRole.Value.GroupId != null)
            {
                var groupRole = await _groupRepository.GetByIdAsync(newRole.Value.GroupId.Value, cancellationToken);
                if (groupRole.IsFailure) return groupRole;

                if (groupRole.Value.Id != userInGroup.Value.GroupId) return Result.Failure(CommandErrors.RoleBelongsToAnotherGroup);
            }

            var updateRole = userInGroup.Value.UpdateRole(newRole.Value);
            if (updateRole.IsFailure) return updateRole;

            var update = await _userInGroupRepository.UpdateAsync(userInGroup.Value, cancellationToken);
            if (update.IsFailure) return update;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
