using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.User.Commands.UserVerifyEmail
{
	internal sealed class UserVerifyEmailCommandHandler : ICommandHandler<UserVerifyEmailCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public UserVerifyEmailCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
		}

		public async Task<Result<Guid>> Handle(UserVerifyEmailCommand request, CancellationToken cancellationToken)
		{
			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var user = foundUserResult.Value;

			user.VerifyEmail();

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}
