using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Abstractions;
using StudyTaskManager.Domain.Abstractions.Repositories;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.User.Commands.UserVerifyPhoneNumber
{
	internal sealed record UserVerifyPhoneNumberCommandHandler : ICommandHandler<UserVerifyPhoneNumberCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepository _userRepository;

		public UserVerifyPhoneNumberCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
		}

		public async Task<Result<Guid>> Handle(UserVerifyPhoneNumberCommand request, CancellationToken cancellationToken)
		{
			var foundUserResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
			if (foundUserResult.IsFailure)
			{
				return Result.Failure<Guid>(foundUserResult.Error);
			}

			var user = foundUserResult.Value;

			user.VerifyPhoneNumber();

			await _userRepository.UpdateAsync(user, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}