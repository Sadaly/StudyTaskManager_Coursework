using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId
{
    public class UserInGroupsGetByUserIdQueryHandler : IQueryHandler<UserInGroupsGetByUserIdQuery, List<UserInGroupsResponse>>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IUserRepository _userRepository;

        public UserInGroupsGetByUserIdQueryHandler(IUserInGroupRepository userInGroupRepository, IUserRepository userRepository)
        {
            _userInGroupRepository = userInGroupRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserInGroupsResponse>>> Handle(UserInGroupsGetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(user);

            var listUIG = await _userInGroupRepository.GetByUserAsync(request.StartIndex, request.Count, user.Value, cancellationToken);
            if (listUIG.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(listUIG);

            var listRes = listUIG.Value.Select(uig => new UserInGroupsResponse(uig)).ToList();

            return listRes;
        }
    }
}
