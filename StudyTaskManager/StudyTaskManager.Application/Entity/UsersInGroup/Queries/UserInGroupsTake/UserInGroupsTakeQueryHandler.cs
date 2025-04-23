using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.TakeUserInGroups
{
    internal class UserInGroupsTakeQueryHandler : IQueryHandler<UserInGroupsTakeQuery, List<UserInGroupsResponse>>
    {
        private readonly IUserInGroupRepository _userInGroupRepository;

        public UserInGroupsTakeQueryHandler(IUserInGroupRepository userInGroupRepository)
        {
            _userInGroupRepository = userInGroupRepository;
        }

        public async Task<Result<List<UserInGroupsResponse>>> Handle(UserInGroupsTakeQuery request, CancellationToken cancellationToken)
        {
            var listUIG = request.Predicate == null
                ? await _userInGroupRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken)
                : await _userInGroupRepository.GetAllAsync(request.StartIndex, request.Count, request.Predicate, cancellationToken);

            if (listUIG.IsFailure) return Result.Failure<List<UserInGroupsResponse>>(listUIG);

            var listRes = listUIG.Value.Select(uig => new UserInGroupsResponse(uig)).ToList();

            return listRes;
        }
    }
}
