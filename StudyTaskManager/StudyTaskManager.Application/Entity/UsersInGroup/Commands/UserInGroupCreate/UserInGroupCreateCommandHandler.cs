using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.Group;
using MediatR;
using StudyTaskManager.Domain.Errors;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupCreate
{
    internal class UserInGroupCreateCommandHandler : ICommandHandler<UserInGroupCreateCommand, UserInGroup>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupRoleRepository _groupRoleRepository;
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupCreateCommandHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository, IGroupRepository groupRepository, IUserRepository userRepository, IGroupRoleRepository groupRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<Result<UserInGroup>> Handle(UserInGroupCreateCommand request, CancellationToken cancellationToken)
        {
            Guid UserId = request.UserId;
            Guid GroupId = request.GroupId;
            Guid RoleId = request.RoleId;

            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<UserInGroup>(user.Error);
            if (user.Value == null) return Result.Failure<UserInGroup>(PersistenceErrors.User.NotFound);

            var group = await _groupRepository.GetByIdAsync(GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<UserInGroup>(group.Error);
            if (group.Value == null) return Result.Failure<UserInGroup>(PersistenceErrors.Group.NotFound);

            var groupRole = await _groupRoleRepository.GetByIdAsync(RoleId, cancellationToken);
            if (groupRole.IsFailure) return Result.Failure<UserInGroup>(groupRole.Error);
            if (groupRole.Value == null) return Result.Failure<UserInGroup>(PersistenceErrors.GroupRole.NotFound);

            Result<UserInGroup> userInGroup = UserInGroup.Create(group.Value, groupRole.Value, user.Value);
            if (userInGroup.IsFailure) return userInGroup;

            Result add = await _userInGroupRepository.AddAsync(userInGroup.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<UserInGroup>(add.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userInGroup;
        }
    }
}
