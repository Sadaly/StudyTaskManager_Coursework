using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds
{
    internal sealed class UserInGroupGetByUserAndGroupIdsQueryHandler : IQueryHandler<UserInGroupGetByUserAndGroupIdsQuery, UserInGroupResponse>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public UserInGroupGetByUserAndGroupIdsQueryHandler(IUserInGroupRepository userInGroupRepository, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result<UserInGroupResponse>> Handle(
            UserInGroupGetByUserAndGroupIdsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<UserInGroupResponse>(user.Error);

            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<UserInGroupResponse>(group.Error);

            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(user.Value, group.Value, cancellationToken);
            if (userInGroup.IsFailure) return Result.Failure<UserInGroupResponse>(userInGroup.Error);

            return Result.Success(new UserInGroupResponse(userInGroup.Value));
        }
    }
}
