using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds
{
    internal sealed class UserInGroupGetByUserAndGroupIdsQueryHandler : IQueryHandler<UserInGroupGetByUserAndGroupIdsQuery, UserInGroupGetByUserAndGroupIdsResponse>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupGetByUserAndGroupIdsQueryHandler(IUserInGroupRepository userInGroupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<UserInGroupGetByUserAndGroupIdsResponse>> Handle(UserInGroupGetByUserAndGroupIdsQuery request, CancellationToken cancellationToken)
        {
            var userInGroup = await _userInGroupRepository.GetByUserAndGroupAsync(request.UserId, request.GroupId, cancellationToken);
            if (userInGroup.IsFailure) return Result.Failure<UserInGroupGetByUserAndGroupIdsResponse>(userInGroup);

            return Result.Success(new UserInGroupGetByUserAndGroupIdsResponse(userInGroup.Value));
        }
    }
}
