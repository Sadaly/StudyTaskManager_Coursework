using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries.GetUserById;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.Users.Queries
{
    internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserResponse>(new Error(
                    "User.NotFound",
                    $"Пользователь {request.UserId} не найден"));
            }

            var response = new UserResponse(user.Value.Id, user.Value.Email.Value);

            return response;
        }
    }
}
