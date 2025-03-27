using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Errors;
using StudyTaskManager.Domain.Shared;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.User.Commands.UserUpdatePassword
{
	internal sealed class UserUpdatePasswordCommandHandler : ICommandHandler<UserUpdatePasswordCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public UserUpdatePasswordCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
		}

		public async Task<Result<Guid>> Handle(UserUpdatePasswordCommand request, CancellationToken cancellationToken)
		{
			Result<Password> usernameResult = Password.Create(request.NewPassword);

			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var user = foundUserResult.Value;
			var changePasswordResult = user.ChangePassword(usernameResult.Value);

			if (changePasswordResult.IsFailure)
			{
				return Result.Failure<Guid>(changePasswordResult.Error);
			}

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}
