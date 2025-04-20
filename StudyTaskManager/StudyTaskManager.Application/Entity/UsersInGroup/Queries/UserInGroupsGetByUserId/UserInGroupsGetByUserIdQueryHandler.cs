using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId
{
    class UserInGroupsGetByUserIdQueryHandler : IQueryHandler<UserInGroupsGetByUserIdQuery, List<UserInGroupsGetByUserIdResponseElements>>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IUserRepository _userRepository;

        public UserInGroupsGetByUserIdQueryHandler(IUserInGroupRepository userInGroupRepository, IUserRepository userRepository)
        {
            _userInGroupRepository = userInGroupRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<UserInGroupsGetByUserIdResponseElements>>> Handle(UserInGroupsGetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user.IsFailure) return Result.Failure<List<UserInGroupsGetByUserIdResponseElements>>(user);

            var listUIG = await _userInGroupRepository.GetByUserAsync(request.StartIndex, request.Count, user.Value, cancellationToken);
            if (listUIG.IsFailure) return Result.Failure<List<UserInGroupsGetByUserIdResponseElements>>(listUIG);

            var listRes = listUIG.Value.Select(uig => new UserInGroupsGetByUserIdResponseElements(uig)).ToList();

            return Result.Success(listRes);
        }
    }
}
