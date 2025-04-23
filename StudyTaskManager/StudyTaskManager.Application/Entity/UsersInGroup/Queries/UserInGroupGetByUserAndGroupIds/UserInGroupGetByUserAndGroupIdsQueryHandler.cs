using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds
{
    internal sealed class UserInGroupGetByUserAndGroupIdsQueryHandler : IQueryHandler<UserInGroupGetByUserAndGroupIdsQuery, UserInGroupsResponse>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupGetByUserAndGroupIdsQueryHandler(IUserInGroupRepository userInGroupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<UserInGroupsResponse>> Handle(UserInGroupGetByUserAndGroupIdsQuery request, CancellationToken cancellationToken)
        {
            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return Result.Failure<UserInGroupsResponse>(userInGroup);

            return new UserInGroupsResponse(
                userInGroup.Value.UserId, 
                userInGroup.Value.GroupId, 
                userInGroup.Value.RoleId, 
                userInGroup.Value.DateEntered);
        }
    }
}
