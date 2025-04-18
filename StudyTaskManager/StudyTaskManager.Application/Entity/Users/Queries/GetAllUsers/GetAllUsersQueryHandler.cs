using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById
{
    internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UserListResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserListResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersResult = await _userRepository.GetAllAsync(cancellationToken);

            if (usersResult.IsFailure) return Result.Failure<UserListResponse>(usersResult.Error);

            return new UserListResponse(usersResult.Value);
        }
    }
}