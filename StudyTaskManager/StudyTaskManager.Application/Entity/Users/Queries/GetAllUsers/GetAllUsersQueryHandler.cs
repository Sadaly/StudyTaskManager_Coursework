using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries.GetAllUsers;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById
{
    internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<GetAllUsersResponseElements>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<GetAllUsersResponseElements>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(request.StartIndex, request.Count, cancellationToken);
            if (users.IsFailure) return Result.Failure<List<GetAllUsersResponseElements>>(users.Error);

            var listRes = users.Value.Select(u => new GetAllUsersResponseElements(u)).ToList();

            return Result.Success(listRes);
        }
    }
}