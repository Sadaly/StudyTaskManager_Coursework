using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.Group;
using MediatR;
using StudyTaskManager.Domain.Errors;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupCreate
{
    internal class UserInGroupCreateWithRoleHandler : ICommandHandler<UserInGroupCreateWithRoleCommand, UserInGroup>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupCreateWithRoleHandler(IUnitOfWork unitOfWork, IUserInGroupRepository userInGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<UserInGroup>> Handle(UserInGroupCreateWithRoleCommand request, CancellationToken cancellationToken)
        {
            Result<UserInGroup> userInGroup = UserInGroup.Create(request.GroupId, request.UserId, request.RoleId);
            if (userInGroup.IsFailure) return userInGroup;

            Result add = await _userInGroupRepository.AddAsync(userInGroup.Value, cancellationToken);
            if (add.IsFailure) return Result.Failure<UserInGroup>(add.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userInGroup;
        }
    }
}
