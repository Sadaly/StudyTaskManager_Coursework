using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Errors;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds
{
    internal sealed class UserInGroupGetByUserAndGroupIdsQueryHandler : IQueryHandler<UserInGroupGetByUserAndGroupIdsQuery, UserInGroupResponse>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupGetByUserAndGroupIdsQueryHandler(IUserInGroupRepository userInGroupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<UserInGroupResponse>> Handle(UserInGroupGetByUserAndGroupIdsQuery request, CancellationToken cancellationToken)
        {
            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return Result.Failure<UserInGroupResponse>(userInGroup.Error);
            if (userInGroup.Value == null) return Result.Failure<UserInGroupResponse>(PersistenceErrors.UserInGroup.NotFound);

            return Result.Success(new UserInGroupResponse(userInGroup.Value));
        }
    }
}
