using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Users.Queries;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.User.Queries.UserGetByUsername
{
	internal sealed class UserGetByUsernameQueryHandler : IQueryHandler<UserGetByUsernameQuery, UserResponse>
	{
		private readonly IUserRepository _userRepository;

		public UserGetByUsernameQueryHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Result<UserResponse>> Handle(
			UserGetByUsernameQuery request,
			CancellationToken cancellationToken)
		{
			var usernameResult = Username.Create(request.Username);

			if (usernameResult.IsFailure)
				return Result.Failure<UserResponse>(usernameResult.Error);

			var userResult = await _userRepository.GetByUsername(
				usernameResult.Value,
				cancellationToken);

			if (userResult.IsFailure)
			{
				return Result.Failure<UserResponse>(userResult.Error);
			}

			var response = new UserResponse(userResult.Value.Id, userResult.Value.Email.Value);

			return Result.Success(response);
		}
	}
}
