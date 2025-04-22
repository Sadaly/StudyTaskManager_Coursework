using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries.UserGetById;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Queries.GetUserById
{
    internal sealed class UserGetByIdQueryHandler : IQueryHandler<UserGetByIdQuery, UserGetByIdResponse>
    {
        private readonly IUserRepository _userRepository;

        public UserGetByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserGetByIdResponse>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (userResult.IsFailure) return Result.Failure<UserGetByIdResponse>(userResult.Error);

            var response = new UserGetByIdResponse(userResult.Value);

            return Result.Success(response);
        }
    }
}
