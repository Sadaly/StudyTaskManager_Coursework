using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByGroupId
{
    public class UserInGroupsGetByGroupIdQueryHandler : IQueryHandler<UserInGroupsGetByGroupIdQuery, List<UserInGroupsResponse>>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;
        private readonly IGroupRepository _groupRepository;

        public UserInGroupsGetByGroupIdQueryHandler(IUserInGroupRepository userInGroupRepository, IGroupRepository groupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
            _groupRepository = groupRepository;
        }

        public async Task<Result<List<UserInGroupsResponse>>> Handle(UserInGroupsGetByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(group);

            var listUIG = await _userInGroupRepository.GetByGroupAsync(request.StartIndex, request.Count, group.Value, cancellationToken);
            if (listUIG.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(listUIG);

            var listRes = listUIG.Value.Select(uig => new UserInGroupsResponse(uig)).ToList();

            return listRes;
        }
    }
}
