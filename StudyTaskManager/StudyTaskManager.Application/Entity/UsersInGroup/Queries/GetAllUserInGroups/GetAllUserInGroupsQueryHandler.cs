using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.GetAllUserInGroups
{
    //todo
    internal class GetAllUserInGroupsQueryHandler : IQueryHandler<GetAllUserInGroupsQuery, List<UserInGroupsResponse>>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;

        public GetAllUserInGroupsQueryHandler(IUserInGroupRepository userInGroupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<List<UserInGroupsResponse>>> Handle(GetAllUserInGroupsQuery request, CancellationToken cancellationToken)
        {
            var listUIG = request.Predicate == null
                ? await _userInGroupRepository.GetAllAsync(cancellationToken)
                : await _userInGroupRepository.GetAllAsync(request.Predicate, cancellationToken);
            if (listUIG.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(listUIG);

            var listRes = listUIG.Value.Select(uig => new UserInGroupsResponse(uig)).ToList();

            return listRes;
        }
    }
}
